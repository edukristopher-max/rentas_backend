using backend.Data;
using backend.dto;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class CajaService : ICajaService
    {
        private readonly AppDbContext _context;

        public CajaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CajaAperturaResponseDto> AbrirCajaAsync(AbrirCajaDto dto)
        {
            var usuario = await _context.Usuarios.FindAsync(dto.UsuarioId);
            if (usuario == null)
            {
                throw new Exception("Usuario no encontrado.");
            }

            var cajaExistente = await _context.CajasAperturas
                .FirstOrDefaultAsync(c => c.UsuarioId == dto.UsuarioId && c.Estado == "Abierta");
            
            if (cajaExistente != null)
            {
                throw new Exception("El usuario ya tiene una caja abierta.");
            }

            var apertura = new CajaApertura
            {
                UsuarioId = dto.UsuarioId,
                MontoApertura = dto.MontoApertura,
                Estado = "Abierta",
                FechaApertura = DateTime.UtcNow
            };

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.CajasAperturas.Add(apertura);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw new Exception("No se pudo abrir la caja debido a un conflicto. Verifica si ya existe una caja abierta.");
            }

            return MapToResponse(apertura);
        }

        public async Task<CajaAperturaResponseDto?> ObtenerCajaAbiertaAsync(int usuarioId)
        {
            var caja = await _context.CajasAperturas
                .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId && c.Estado == "Abierta");
            
            return caja == null ? null : MapToResponse(caja);
        }

        public async Task<CajaMovimientoResponseDto> RegistrarMovimientoAsync(RegistrarMovimientoDto dto)
        {
            if (dto.Monto <= 0)
            {
                throw new Exception("El monto del movimiento debe ser mayor a 0.");
            }

            var validTipos = new[] { "Ingreso", "Egreso" };
            if (!validTipos.Contains(dto.Tipo))
            {
                throw new Exception("El tipo de movimiento solo puede ser 'Ingreso' o 'Egreso'.");
            }

            var validMetodos = new[] { "Efectivo", "Tarjeta de Crédito", "Transferencia", "Yape/Plin" };
            if (!validMetodos.Contains(dto.MetodoPago))
            {
                throw new Exception("El método de pago no es válido.");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var caja = await _context.CajasAperturas
                    .FromSqlRaw("SELECT * FROM CajasAperturas WITH (UPDLOCK, ROWLOCK) WHERE Id = {0}", dto.CajaAperturaId)
                    .FirstOrDefaultAsync();

                if (caja == null)
                {
                    throw new Exception("Caja no encontrada.");
                }
                
                if (caja.Estado != "Abierta")
                {
                    throw new Exception("No se pueden registrar movimientos en una caja cerrada.");
                }

                var movimiento = new CajaMovimiento
                {
                    CajaAperturaId = dto.CajaAperturaId,
                    Monto = dto.Monto,
                    Tipo = dto.Tipo,
                    MetodoPago = dto.MetodoPago,
                    PagoId = dto.PagoId,
                    FechaHora = DateTime.UtcNow
                };

                _context.CajasMovimientos.Add(movimiento);
                await _context.SaveChangesAsync();
                
                await transaction.CommitAsync();
                
                return MapToResponse(movimiento);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<CajaMovimientoResponseDto>> ObtenerMovimientosAsync(int cajaAperturaId)
        {
            var movimientos = await _context.CajasMovimientos
                .Where(m => m.CajaAperturaId == cajaAperturaId)
                .OrderBy(m => m.FechaHora)
                .ToListAsync();

            return movimientos.Select(MapToResponse);
        }

        public async Task<CajaCierreResponseDto> CerrarCajaAsync(CerrarCajaDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var caja = await _context.CajasAperturas
                    .FromSqlRaw("SELECT * FROM CajasAperturas WITH (UPDLOCK, ROWLOCK) WHERE Id = {0}", dto.CajaAperturaId)
                    .FirstOrDefaultAsync();

                if (caja == null)
                {
                    throw new Exception("Caja no encontrada.");
                }

                if (caja.Estado != "Abierta")
                {
                    throw new Exception("Solo se pueden cerrar cajas en estado 'Abierta'.");
                }

                caja.Movimientos = await _context.CajasMovimientos
                    .Where(m => m.CajaAperturaId == caja.Id)
                    .ToListAsync();

                decimal montoSistema = caja.MontoApertura;
                foreach (var mov in caja.Movimientos)
                {
                    if (mov.Tipo == "Ingreso") montoSistema += mov.Monto;
                    else if (mov.Tipo == "Egreso") montoSistema -= mov.Monto;
                }

                decimal efectivoEsperado = caja.MontoApertura;
                foreach (var mov in caja.Movimientos.Where(m => m.MetodoPago == "Efectivo"))
                {
                    if (mov.Tipo == "Ingreso") efectivoEsperado += mov.Monto;
                    else if (mov.Tipo == "Egreso") efectivoEsperado -= mov.Monto;
                }

                decimal diferencia = dto.MontoDeclarado - efectivoEsperado;

                var cierre = new CajaCierre
                {
                    CajaAperturaId = dto.CajaAperturaId,
                    MontoSistema = montoSistema,
                    MontoDeclarado = dto.MontoDeclarado,
                    Diferencia = diferencia,
                    UsuarioResponsableId = dto.UsuarioResponsableId,
                    Observaciones = dto.Observaciones,
                    FechaCierre = DateTime.UtcNow
                };

                _context.CajasCierres.Add(cierre);

                caja.Estado = "Cerrada";
                _context.CajasAperturas.Update(caja);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new CajaCierreResponseDto
                {
                    Id = cierre.Id,
                    CajaAperturaId = cierre.CajaAperturaId,
                    FechaCierre = cierre.FechaCierre,
                    MontoSistema = cierre.MontoSistema,
                    MontoDeclarado = cierre.MontoDeclarado,
                    Diferencia = cierre.Diferencia,
                    UsuarioResponsableId = cierre.UsuarioResponsableId,
                    Observaciones = cierre.Observaciones
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception(ex.Message);
            }
        }

        private static CajaAperturaResponseDto MapToResponse(CajaApertura caja)
        {
            return new CajaAperturaResponseDto
            {
                Id = caja.Id,
                UsuarioId = caja.UsuarioId,
                FechaApertura = caja.FechaApertura,
                MontoApertura = caja.MontoApertura,
                Estado = caja.Estado
            };
        }

        private static CajaMovimientoResponseDto MapToResponse(CajaMovimiento movimiento)
        {
            return new CajaMovimientoResponseDto
            {
                Id = movimiento.Id,
                CajaAperturaId = movimiento.CajaAperturaId,
                PagoId = movimiento.PagoId,
                Tipo = movimiento.Tipo,
                Monto = movimiento.Monto,
                FechaHora = movimiento.FechaHora,
                MetodoPago = movimiento.MetodoPago
            };
        }
    }
}
