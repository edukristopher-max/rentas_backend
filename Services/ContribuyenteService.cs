using backend.Data;
using backend.dto;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class ContribuyenteService : IContribuyenteService
    {
        private readonly AppDbContext _context;

        public ContribuyenteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contribuyente>> GetContribuyentesAsync()
        {
            return await _context.Contribuyentes.ToListAsync();
        }

        public async Task<Contribuyente?> GetContribuyenteByIdAsync(int id)
        {
            return await _context.Contribuyentes
                .Include(c => c.Predios!)
                .ThenInclude(p => p.ArbitriosMunicipales)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Contribuyente?> GetContribuyenteByDocumentoAsync(string numeroDocumento)
        {
            return await _context.Contribuyentes
                .Include(c => c.Predios!)
                .ThenInclude(p => p.ArbitriosMunicipales)
                .FirstOrDefaultAsync(c => c.NumeroDocumento == numeroDocumento);
        }

        public async Task<Contribuyente> CreateContribuyenteAsync(CreateContribuyenteDto dto)
        {
            var contribuyente = new Contribuyente
            {
                TipoDocumento = dto.TipoDocumento,
                NumeroDocumento = dto.NumeroDocumento,
                NombreCompletoRazonSocial = dto.NombreCompletoRazonSocial,
                Direccion = dto.Direccion,
                Telefono = dto.Telefono,
                Email = dto.Email,
                Estado = true
            };

            _context.Contribuyentes.Add(contribuyente);
            await _context.SaveChangesAsync();

            return contribuyente;
        }

        public async Task<Contribuyente?> UpdateContribuyenteAsync(int id, UpdateContribuyenteDto dto)
        {
            var contribuyente = await _context.Contribuyentes.FindAsync(id);

            if (contribuyente == null)
            {
                return null;
            }

            contribuyente.TipoDocumento = dto.TipoDocumento;
            contribuyente.NumeroDocumento = dto.NumeroDocumento;
            contribuyente.NombreCompletoRazonSocial = dto.NombreCompletoRazonSocial;
            contribuyente.Direccion = dto.Direccion;
            contribuyente.Telefono = dto.Telefono;
            contribuyente.Email = dto.Email;
            contribuyente.Estado = dto.Estado;

            _context.Contribuyentes.Update(contribuyente);
            await _context.SaveChangesAsync();

            return contribuyente;
        }

        public async Task<bool> DeleteContribuyenteAsync(int id)
        {
            var contribuyente = await _context.Contribuyentes.FindAsync(id);

            if (contribuyente == null)
            {
                return false;
            }

            _context.Contribuyentes.Remove(contribuyente);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
