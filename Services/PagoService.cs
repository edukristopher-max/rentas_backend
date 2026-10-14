using backend.Data;
using backend.dto;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace backend.Services
{
    public class PagoService : IPagoService
    {
        private readonly AppDbContext _context;

        public PagoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PagoResponseDto>> GetPagosAsync()
        {
            var pagos = await _context.Pagos
                .Include(p => p.Deuda)
                    .ThenInclude(d => d!.Contribuyente)
                .Where(p => p.Estado == "Vigente")
                .OrderByDescending(p => p.FechaPago)
                .ToListAsync();

            return pagos.Select(MapToResponseDto);
        }

        public async Task<PagoResponseDto?> GetPagoByIdAsync(int id)
        {
            var pago = await _context.Pagos
                .Include(p => p.Deuda)
                    .ThenInclude(d => d!.Contribuyente)
                .FirstOrDefaultAsync(p => p.Id == id && p.Estado == "Vigente");

            if (pago == null) return null;

            return MapToResponseDto(pago);
        }

        private async Task<string> GenerarNumeroReciboDbAsync()
        {
            var conn = _context.Database.GetDbConnection();
            if (conn.State != ConnectionState.Open) await conn.OpenAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT NEXT VALUE FOR Seq_NumeroRecibo";
            cmd.Transaction = _context.Database.CurrentTransaction?.GetDbTransaction();
            var seq = await cmd.ExecuteScalarAsync();
            return $"REC-{DateTime.Now.Year}-{Convert.ToInt32(seq):D6}";
        }

        public async Task<PagoResponseDto> CreatePagoAsync(CreatePagoDto dto)
        {
            var deuda = await _context.Deudas.FindAsync(dto.DeudaId);
            if (deuda == null) throw new Exception("Deuda no encontrada.");
            if (deuda.Estado == "Pagada") throw new Exception("La deuda ya se encuentra pagada.");

            throw new Exception("No existe correspondencia inequívoca para esta Deuda. El método V1 está deprecado para crear nuevos pagos. Por favor utilice el endpoint /api/pagos/v2.");
        }

        public async Task<PagoConfirmadoResponseDto> CreatePagoV2Async(CrearPagoV2Dto dto)
        {
            if (dto.Detalles == null || !dto.Detalles.Any())
                throw new Exception("El pago debe tener al menos un detalle.");

            if (dto.Detalles.Any(d => d.MontoAplicado <= 0))
                throw new Exception("Los montos aplicados deben ser mayores a cero.");

            if (dto.Detalles.GroupBy(d => d.ObligacionId).Any(g => g.Count() > 1))
                throw new Exception("No se permiten obligaciones duplicadas en el mismo pago.");

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var caja = await _context.CajasAperturas
                    .FromSqlRaw("SELECT * FROM CajasAperturas WITH (UPDLOCK, ROWLOCK) WHERE Id = {0}", dto.CajaAperturaId)
                    .FirstOrDefaultAsync();

                if (caja == null) throw new Exception("Caja no encontrada.");
                if (caja.Estado != "Abierta") throw new Exception("No se pueden registrar pagos en una caja cerrada.");

                decimal totalPago = dto.Detalles.Sum(d => d.MontoAplicado);
                var obligacionesAfectadas = new List<string>();
                var pagoDetalles = new List<PagoDetalle>();

                foreach (var det in dto.Detalles)
                {
                    var obligacion = await _context.Obligaciones.FindAsync(det.ObligacionId);
                    if (obligacion == null)
                        throw new Exception($"Obligacion {det.ObligacionId} no encontrada.");

                    if (obligacion.Estado == "Anulada")
                        throw new Exception($"La obligación {det.ObligacionId} está anulada.");

                    if (obligacion.Estado == "Pagada")
                        throw new Exception($"La obligación {det.ObligacionId} ya está pagada.");

                    if (det.MontoAplicado > obligacion.Saldo)
                        throw new Exception($"El monto a pagar ({det.MontoAplicado}) supera el saldo disponible ({obligacion.Saldo}) en la obligación {det.ObligacionId}.");

                    obligacion.Pagado += det.MontoAplicado;
                    obligacion.Saldo -= det.MontoAplicado;
                    if (Math.Round(obligacion.Saldo, 2) <= 0)
                    {
                        obligacion.Estado = "Pagada";
                    }
                    else if (Math.Round(obligacion.Pagado, 2) > 0)
                    {
                        obligacion.Estado = "Parcial";
                    }

                    pagoDetalles.Add(new PagoDetalle
                    {
                        ObligacionId = obligacion.Id,
                        Monto = det.MontoAplicado
                    });

                    obligacionesAfectadas.Add(obligacion.Periodo);
                }

                string numeroRecibo = await GenerarNumeroReciboDbAsync();

                var pago = new Pago
                {
                    DeudaId = null,
                    NumeroRecibo = numeroRecibo,
                    FechaPago = DateTime.Now,
                    MontoPagado = totalPago,
                    MetodoPago = dto.MetodoPago,
                    Cajero = dto.UsuarioCajeroId.ToString(),
                    Notas = dto.Notas,
                    Estado = "Vigente",
                    PagoDetalles = pagoDetalles
                };

                _context.Pagos.Add(pago);
                await _context.SaveChangesAsync();

                var recibo = new Recibo
                {
                    PagoId = pago.Id,
                    NumeroComprobante = numeroRecibo,
                    FechaEmision = DateTime.Now,
                    Estado = "Emitido"
                };
                _context.Recibos.Add(recibo);

                var movimiento = new CajaMovimiento
                {
                    CajaAperturaId = caja.Id,
                    PagoId = pago.Id,
                    Tipo = "Ingreso",
                    Monto = totalPago,
                    FechaHora = DateTime.Now,
                    MetodoPago = dto.MetodoPago
                };
                _context.CajasMovimientos.Add(movimiento);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new PagoConfirmadoResponseDto
                {
                    PagoId = pago.Id,
                    NumeroRecibo = numeroRecibo,
                    TotalPagado = totalPago,
                    MetodoPago = pago.MetodoPago,
                    FechaPago = pago.FechaPago,
                    ObligacionesAfectadas = obligacionesAfectadas,
                    CajaMovimientoId = movimiento.Id
                };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private static PagoResponseDto MapToResponseDto(Pago pago)
        {
            return new PagoResponseDto
            {
                Id = pago.Id,
                DeudaId = pago.DeudaId,
                NumeroRecibo = pago.NumeroRecibo,
                FechaPago = pago.FechaPago,
                MontoPagado = pago.MontoPagado,
                MetodoPago = pago.MetodoPago,
                Cajero = pago.Cajero,
                Notas = pago.Notas,
                DeudaConcepto = pago.Deuda?.Concepto ?? "Desconocido",
                ContribuyenteNombre = pago.Deuda?.Contribuyente?.NombreCompletoRazonSocial ?? "Desconocido"
            };
        }
    }
}
