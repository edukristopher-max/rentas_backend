using backend.Data;
using backend.dto;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class ArbitrioMunicipalService : IArbitrioMunicipalService
    {
        private readonly AppDbContext _context;

        public ArbitrioMunicipalService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ArbitrioMunicipalResponseDto>> GetArbitriosAsync()
        {
            var arbitrios = await _context.ArbitriosMunicipales
                .Include(a => a.Predio)
                .ToListAsync();

            return arbitrios.Select(MapToResponseDto);
        }

        public async Task<ArbitrioMunicipalResponseDto?> GetArbitrioByIdAsync(int id)
        {
            var arbitrio = await _context.ArbitriosMunicipales
                .Include(a => a.Predio)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (arbitrio == null) return null;

            return MapToResponseDto(arbitrio);
        }

        public async Task<ArbitrioMunicipalResponseDto> CreateArbitrioAsync(CreateArbitrioMunicipalDto dto)
        {
            var arbitrio = new ArbitrioMunicipal
            {
                PredioId = dto.PredioId,
                AnioFiscal = dto.AnioFiscal,
                Periodo = dto.Periodo,
                MontoLimpieza = dto.MontoLimpieza,
                MontoParques = dto.MontoParques,
                MontoSerenazgo = dto.MontoSerenazgo,
                MontoTotal = dto.MontoLimpieza + dto.MontoParques + dto.MontoSerenazgo,
                FechaVencimiento = dto.FechaVencimiento,
                EstadoPago = dto.EstadoPago
            };

            _context.ArbitriosMunicipales.Add(arbitrio);
            await _context.SaveChangesAsync();

            arbitrio = await _context.ArbitriosMunicipales
                .Include(a => a.Predio)
                .FirstOrDefaultAsync(a => a.Id == arbitrio.Id);

            return MapToResponseDto(arbitrio!);
        }

        public async Task<ArbitrioMunicipalResponseDto?> UpdateArbitrioAsync(int id, UpdateArbitrioMunicipalDto dto)
        {
            var arbitrio = await _context.ArbitriosMunicipales
                .Include(a => a.Predio)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (arbitrio == null) return null;

            arbitrio.PredioId = dto.PredioId;
            arbitrio.AnioFiscal = dto.AnioFiscal;
            arbitrio.Periodo = dto.Periodo;
            arbitrio.MontoLimpieza = dto.MontoLimpieza;
            arbitrio.MontoParques = dto.MontoParques;
            arbitrio.MontoSerenazgo = dto.MontoSerenazgo;
            arbitrio.MontoTotal = dto.MontoLimpieza + dto.MontoParques + dto.MontoSerenazgo;
            arbitrio.FechaVencimiento = dto.FechaVencimiento;
            arbitrio.EstadoPago = dto.EstadoPago;
            arbitrio.FechaPago = dto.FechaPago;

            _context.ArbitriosMunicipales.Update(arbitrio);
            await _context.SaveChangesAsync();

            arbitrio = await _context.ArbitriosMunicipales
                .Include(a => a.Predio)
                .FirstOrDefaultAsync(a => a.Id == id);

            return MapToResponseDto(arbitrio!);
        }

        public async Task<bool> DeleteArbitrioAsync(int id)
        {
            var arbitrio = await _context.ArbitriosMunicipales.FindAsync(id);
            if (arbitrio == null) return false;

            _context.ArbitriosMunicipales.Remove(arbitrio);
            await _context.SaveChangesAsync();
            return true;
        }

        private static ArbitrioMunicipalResponseDto MapToResponseDto(ArbitrioMunicipal arbitrio)
        {
            return new ArbitrioMunicipalResponseDto
            {
                Id = arbitrio.Id,
                PredioId = arbitrio.PredioId,
                AnioFiscal = arbitrio.AnioFiscal,
                Periodo = arbitrio.Periodo,
                MontoLimpieza = arbitrio.MontoLimpieza,
                MontoParques = arbitrio.MontoParques,
                MontoSerenazgo = arbitrio.MontoSerenazgo,
                MontoTotal = arbitrio.MontoTotal,
                FechaVencimiento = arbitrio.FechaVencimiento,
                EstadoPago = arbitrio.EstadoPago,
                FechaPago = arbitrio.FechaPago,
                PredioCodigoCatastral = arbitrio.Predio?.CodigoCatastral ?? "Desconocido",
                PredioDireccion = arbitrio.Predio?.Direccion ?? "Desconocido"
            };
        }
    }
}
