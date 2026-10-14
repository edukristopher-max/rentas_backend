using backend.Data;
using backend.dto;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class DeudaService : IDeudaService
    {
        private readonly AppDbContext _context;

        public DeudaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DeudaResponseDto>> GetDeudasAsync()
        {
            var deudas = await _context.Deudas
                .Include(d => d.Contribuyente)
                .Include(d => d.Predio)
                .ToListAsync();

            return deudas.Select(MapToResponseDto);
        }

        public async Task<DeudaResponseDto?> GetDeudaByIdAsync(int id)
        {
            var deuda = await _context.Deudas
                .Include(d => d.Contribuyente)
                .Include(d => d.Predio)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (deuda == null) return null;

            return MapToResponseDto(deuda);
        }

        public async Task<DeudaResponseDto> CreateDeudaAsync(CreateDeudaDto dto)
        {
            var deuda = new Deuda
            {
                ContribuyenteId = dto.ContribuyenteId,
                PredioId = dto.PredioId == 0 ? null : dto.PredioId,
                Concepto = dto.Concepto,
                TipoDeuda = dto.TipoDeuda,
                MontoOriginal = dto.MontoOriginal,
                Mora = dto.Mora,
                Descuento = dto.Descuento,
                TotalPagar = (dto.MontoOriginal + dto.Mora) - dto.Descuento,
                FechaVencimiento = dto.FechaVencimiento,
                Estado = dto.Estado
            };

            _context.Deudas.Add(deuda);
            await _context.SaveChangesAsync();

            deuda = await _context.Deudas
                .Include(d => d.Contribuyente)
                .Include(d => d.Predio)
                .FirstOrDefaultAsync(d => d.Id == deuda.Id);

            return MapToResponseDto(deuda!);
        }

        public async Task<DeudaResponseDto?> UpdateDeudaAsync(int id, UpdateDeudaDto dto)
        {
            var deuda = await _context.Deudas
                .Include(d => d.Contribuyente)
                .Include(d => d.Predio)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (deuda == null) return null;

            deuda.ContribuyenteId = dto.ContribuyenteId;
            deuda.PredioId = dto.PredioId == 0 ? null : dto.PredioId;
            deuda.Concepto = dto.Concepto;
            deuda.TipoDeuda = dto.TipoDeuda;
            deuda.MontoOriginal = dto.MontoOriginal;
            deuda.Mora = dto.Mora;
            deuda.Descuento = dto.Descuento;
            deuda.TotalPagar = (dto.MontoOriginal + dto.Mora) - dto.Descuento;
            deuda.FechaVencimiento = dto.FechaVencimiento;
            deuda.Estado = dto.Estado;

            _context.Deudas.Update(deuda);
            await _context.SaveChangesAsync();

            deuda = await _context.Deudas
                .Include(d => d.Contribuyente)
                .Include(d => d.Predio)
                .FirstOrDefaultAsync(d => d.Id == id);

            return MapToResponseDto(deuda!);
        }

        public async Task<bool> DeleteDeudaAsync(int id)
        {
            var deuda = await _context.Deudas.FindAsync(id);
            if (deuda == null) return false;

            _context.Deudas.Remove(deuda);
            await _context.SaveChangesAsync();
            return true;
        }

        private static DeudaResponseDto MapToResponseDto(Deuda deuda)
        {
            return new DeudaResponseDto
            {
                Id = deuda.Id,
                ContribuyenteId = deuda.ContribuyenteId,
                PredioId = deuda.PredioId,
                Concepto = deuda.Concepto,
                TipoDeuda = deuda.TipoDeuda,
                MontoOriginal = deuda.MontoOriginal,
                Mora = deuda.Mora,
                Descuento = deuda.Descuento,
                TotalPagar = deuda.TotalPagar,
                FechaVencimiento = deuda.FechaVencimiento,
                Estado = deuda.Estado,
                ContribuyenteNombre = deuda.Contribuyente?.NombreCompletoRazonSocial ?? "Desconocido",
                PredioDireccion = deuda.Predio?.Direccion ?? ""
            };
        }
    }
}
