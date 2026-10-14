using backend.Data;
using backend.dto;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class LicenciaTasaService : ILicenciaTasaService
    {
        private readonly AppDbContext _context;

        public LicenciaTasaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LicenciaTasaResponseDto>> GetLicenciasAsync()
        {
            var licencias = await _context.LicenciasTasas
                .Include(l => l.Contribuyente)
                .Include(l => l.Predio)
                .ToListAsync();

            return licencias.Select(MapToResponseDto);
        }

        public async Task<LicenciaTasaResponseDto?> GetLicenciaByIdAsync(int id)
        {
            var licencia = await _context.LicenciasTasas
                .Include(l => l.Contribuyente)
                .Include(l => l.Predio)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (licencia == null) return null;

            return MapToResponseDto(licencia);
        }

        public async Task<LicenciaTasaResponseDto> CreateLicenciaAsync(CreateLicenciaTasaDto dto)
        {
            var licencia = new LicenciaTasa
            {
                ContribuyenteId = dto.ContribuyenteId,
                PredioId = dto.PredioId == 0 ? null : dto.PredioId,
                Tipo = dto.Tipo,
                NumeroExpediente = dto.NumeroExpediente,
                Concepto = dto.Concepto,
                Monto = dto.Monto,
                FechaEmision = dto.FechaEmision,
                FechaVencimiento = dto.FechaVencimiento,
                Estado = dto.Estado
            };

            _context.LicenciasTasas.Add(licencia);
            await _context.SaveChangesAsync();

            licencia = await _context.LicenciasTasas
                .Include(l => l.Contribuyente)
                .Include(l => l.Predio)
                .FirstOrDefaultAsync(l => l.Id == licencia.Id);

            return MapToResponseDto(licencia!);
        }

        public async Task<LicenciaTasaResponseDto?> UpdateLicenciaAsync(int id, UpdateLicenciaTasaDto dto)
        {
            var licencia = await _context.LicenciasTasas
                .Include(l => l.Contribuyente)
                .Include(l => l.Predio)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (licencia == null) return null;

            licencia.ContribuyenteId = dto.ContribuyenteId;
            licencia.PredioId = dto.PredioId == 0 ? null : dto.PredioId;
            licencia.Tipo = dto.Tipo;
            licencia.NumeroExpediente = dto.NumeroExpediente;
            licencia.Concepto = dto.Concepto;
            licencia.Monto = dto.Monto;
            licencia.FechaEmision = dto.FechaEmision;
            licencia.FechaVencimiento = dto.FechaVencimiento;
            licencia.Estado = dto.Estado;

            _context.LicenciasTasas.Update(licencia);
            await _context.SaveChangesAsync();

            licencia = await _context.LicenciasTasas
                .Include(l => l.Contribuyente)
                .Include(l => l.Predio)
                .FirstOrDefaultAsync(l => l.Id == id);

            return MapToResponseDto(licencia!);
        }

        public async Task<bool> DeleteLicenciaAsync(int id)
        {
            var licencia = await _context.LicenciasTasas.FindAsync(id);
            if (licencia == null) return false;

            _context.LicenciasTasas.Remove(licencia);
            await _context.SaveChangesAsync();
            return true;
        }

        private static LicenciaTasaResponseDto MapToResponseDto(LicenciaTasa licencia)
        {
            return new LicenciaTasaResponseDto
            {
                Id = licencia.Id,
                ContribuyenteId = licencia.ContribuyenteId,
                PredioId = licencia.PredioId,
                Tipo = licencia.Tipo,
                NumeroExpediente = licencia.NumeroExpediente,
                Concepto = licencia.Concepto,
                Monto = licencia.Monto,
                FechaEmision = licencia.FechaEmision,
                FechaVencimiento = licencia.FechaVencimiento,
                Estado = licencia.Estado,
                ContribuyenteNombre = licencia.Contribuyente?.NombreCompletoRazonSocial ?? "Desconocido",
                PredioDireccion = licencia.Predio?.Direccion ?? ""
            };
        }
    }
}
