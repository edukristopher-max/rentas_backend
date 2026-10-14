using backend.Data;
using backend.dto;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class ActividadRecienteService : IActividadRecienteService
    {
        private readonly AppDbContext _context;

        public ActividadRecienteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ActividadRecienteDto>> GetUltimasActividadesAsync(int limit = 20)
        {
            var actividades = await _context.ActividadesRecientes
                .OrderByDescending(a => a.Fecha)
                .Take(limit)
                .ToListAsync();

            return actividades.Select(a => new ActividadRecienteDto
            {
                Id = a.Id,
                Modulo = a.Modulo,
                Accion = a.Accion,
                Fecha = a.Fecha,
                Usuario = a.Usuario
            });
        }

        public async Task<ActividadRecienteDto> RegistrarActividadAsync(CreateActividadRecienteDto dto)
        {
            var actividad = new ActividadReciente
            {
                Modulo = dto.Modulo,
                Accion = dto.Accion,
                Fecha = DateTime.Now,
                Usuario = "Admin" // Podría venir de un claim JWT
            };

            _context.ActividadesRecientes.Add(actividad);
            await _context.SaveChangesAsync();

            return new ActividadRecienteDto
            {
                Id = actividad.Id,
                Modulo = actividad.Modulo,
                Accion = actividad.Accion,
                Fecha = actividad.Fecha,
                Usuario = actividad.Usuario
            };
        }
    }
}
