using backend.Data;
using backend.dto;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class PredioService : IPredioService
    {
        private readonly AppDbContext _context;

        public PredioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PredioResponseDto>> GetPrediosAsync()
        {
            var predios = await _context.Predios
                .Include(p => p.Contribuyente)
                .ToListAsync();

            return predios.Select(MapToResponseDto);
        }

        public async Task<PredioResponseDto?> GetPredioByIdAsync(int id)
        {
            var predio = await _context.Predios
                .Include(p => p.Contribuyente)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (predio == null) return null;

            return MapToResponseDto(predio);
        }

        public async Task<PredioResponseDto> CreatePredioAsync(CreatePredioDto dto)
        {
            var predio = new Predio
            {
                CodigoCatastral = dto.CodigoCatastral,
                Direccion = dto.Direccion,
                AreaTerreno = dto.AreaTerreno,
                AreaConstruida = dto.AreaConstruida,
                UsoPredio = dto.UsoPredio,
                ValorArancelario = dto.ValorArancelario,
                ContribuyenteId = dto.ContribuyenteId,
                Estado = true
            };

            _context.Predios.Add(predio);
            await _context.SaveChangesAsync();

            // Load Contribuyente for response
            predio = await _context.Predios
                .Include(p => p.Contribuyente)
                .FirstOrDefaultAsync(p => p.Id == predio.Id);

            return MapToResponseDto(predio!);
        }

        public async Task<PredioResponseDto?> UpdatePredioAsync(int id, UpdatePredioDto dto)
        {
            var predio = await _context.Predios
                .Include(p => p.Contribuyente)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (predio == null) return null;

            predio.CodigoCatastral = dto.CodigoCatastral;
            predio.Direccion = dto.Direccion;
            predio.AreaTerreno = dto.AreaTerreno;
            predio.AreaConstruida = dto.AreaConstruida;
            predio.UsoPredio = dto.UsoPredio;
            predio.ValorArancelario = dto.ValorArancelario;
            predio.ContribuyenteId = dto.ContribuyenteId;
            predio.Estado = dto.Estado;

            _context.Predios.Update(predio);
            await _context.SaveChangesAsync();

            // Reload Contribuyente if changed
            predio = await _context.Predios
                .Include(p => p.Contribuyente)
                .FirstOrDefaultAsync(p => p.Id == id);

            return MapToResponseDto(predio!);
        }

        public async Task<bool> DeletePredioAsync(int id)
        {
            var predio = await _context.Predios.FindAsync(id);
            if (predio == null) return false;

            _context.Predios.Remove(predio);
            await _context.SaveChangesAsync();
            return true;
        }

        private static PredioResponseDto MapToResponseDto(Predio predio)
        {
            return new PredioResponseDto
            {
                Id = predio.Id,
                CodigoCatastral = predio.CodigoCatastral,
                Direccion = predio.Direccion,
                AreaTerreno = predio.AreaTerreno,
                AreaConstruida = predio.AreaConstruida,
                UsoPredio = predio.UsoPredio,
                ValorArancelario = predio.ValorArancelario,
                ContribuyenteId = predio.ContribuyenteId,
                Estado = predio.Estado,
                ContribuyenteNombre = predio.Contribuyente?.NombreCompletoRazonSocial ?? "Desconocido",
                ContribuyenteDocumento = predio.Contribuyente?.NumeroDocumento ?? "Desconocido"
            };
        }
    }
}
