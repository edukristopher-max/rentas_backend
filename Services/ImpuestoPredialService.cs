using backend.Data;
using backend.dto;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class ImpuestoPredialService : IImpuestoPredialService
    {
        private readonly AppDbContext _context;

        public ImpuestoPredialService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ImpuestoPredialResponseDto>> GetImpuestosAsync()
        {
            var impuestos = await _context.ImpuestosPrediales
                .Include(i => i.Predio)
                .ToListAsync();

            return impuestos.Select(MapToResponseDto);
        }

        public async Task<ImpuestoPredialResponseDto?> GetImpuestoByIdAsync(int id)
        {
            var impuesto = await _context.ImpuestosPrediales
                .Include(i => i.Predio)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (impuesto == null) return null;

            return MapToResponseDto(impuesto);
        }

        public async Task<ImpuestoPredialResponseDto> CreateImpuestoAsync(CreateImpuestoPredialDto dto)
        {
            var impuesto = new ImpuestoPredial
            {
                PredioId = dto.PredioId,
                AnioFiscal = dto.AnioFiscal,
                MontoBase = dto.MontoBase,
                ImpuestoCalculado = dto.ImpuestoCalculado,
                FechaVencimiento = dto.FechaVencimiento,
                EstadoPago = dto.EstadoPago
            };

            _context.ImpuestosPrediales.Add(impuesto);
            await _context.SaveChangesAsync();

            impuesto = await _context.ImpuestosPrediales
                .Include(i => i.Predio)
                .FirstOrDefaultAsync(i => i.Id == impuesto.Id);

            return MapToResponseDto(impuesto!);
        }

        public async Task<ImpuestoPredialResponseDto?> UpdateImpuestoAsync(int id, UpdateImpuestoPredialDto dto)
        {
            var impuesto = await _context.ImpuestosPrediales
                .Include(i => i.Predio)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (impuesto == null) return null;

            impuesto.PredioId = dto.PredioId;
            impuesto.AnioFiscal = dto.AnioFiscal;
            impuesto.MontoBase = dto.MontoBase;
            impuesto.ImpuestoCalculado = dto.ImpuestoCalculado;
            impuesto.FechaVencimiento = dto.FechaVencimiento;
            impuesto.EstadoPago = dto.EstadoPago;
            impuesto.FechaPago = dto.FechaPago;

            _context.ImpuestosPrediales.Update(impuesto);
            await _context.SaveChangesAsync();

            impuesto = await _context.ImpuestosPrediales
                .Include(i => i.Predio)
                .FirstOrDefaultAsync(i => i.Id == id);

            return MapToResponseDto(impuesto!);
        }

        public async Task<bool> DeleteImpuestoAsync(int id)
        {
            var impuesto = await _context.ImpuestosPrediales.FindAsync(id);
            if (impuesto == null) return false;

            _context.ImpuestosPrediales.Remove(impuesto);
            await _context.SaveChangesAsync();
            return true;
        }

        private static ImpuestoPredialResponseDto MapToResponseDto(ImpuestoPredial impuesto)
        {
            return new ImpuestoPredialResponseDto
            {
                Id = impuesto.Id,
                PredioId = impuesto.PredioId,
                AnioFiscal = impuesto.AnioFiscal,
                MontoBase = impuesto.MontoBase,
                ImpuestoCalculado = impuesto.ImpuestoCalculado,
                FechaVencimiento = impuesto.FechaVencimiento,
                EstadoPago = impuesto.EstadoPago,
                FechaPago = impuesto.FechaPago,
                PredioCodigoCatastral = impuesto.Predio?.CodigoCatastral ?? "Desconocido",
                PredioDireccion = impuesto.Predio?.Direccion ?? "Desconocido"
            };
        }
    }
}
