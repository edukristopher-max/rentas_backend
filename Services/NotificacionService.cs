using backend.Data;
using backend.dto;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class NotificacionService : INotificacionService
    {
        private readonly AppDbContext _context;

        public NotificacionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NotificacionResponseDto>> GetNotificacionesAsync()
        {
            var notificaciones = await _context.Notificaciones
                .Include(n => n.Contribuyente)
                .OrderByDescending(n => n.FechaEnvio)
                .ToListAsync();

            return notificaciones.Select(MapToResponseDto);
        }

        public async Task<NotificacionResponseDto?> GetNotificacionByIdAsync(int id)
        {
            var notificacion = await _context.Notificaciones
                .Include(n => n.Contribuyente)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (notificacion == null) return null;

            return MapToResponseDto(notificacion);
        }

        public async Task<NotificacionResponseDto> CreateNotificacionAsync(CreateNotificacionDto dto)
        {
            var notificacion = new Notificacion
            {
                ContribuyenteId = dto.ContribuyenteId,
                Tipo = dto.Tipo,
                MedioEnvio = dto.MedioEnvio,
                Titulo = dto.Titulo,
                Mensaje = dto.Mensaje,
                FechaEnvio = DateTime.Now,
                Estado = "Enviada" // Estado por defecto
            };

            _context.Notificaciones.Add(notificacion);
            await _context.SaveChangesAsync();

            notificacion = await _context.Notificaciones
                .Include(n => n.Contribuyente)
                .FirstOrDefaultAsync(n => n.Id == notificacion.Id);

            return MapToResponseDto(notificacion!);
        }

        public async Task<NotificacionResponseDto?> UpdateNotificacionAsync(int id, UpdateNotificacionDto dto)
        {
            var notificacion = await _context.Notificaciones
                .Include(n => n.Contribuyente)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (notificacion == null) return null;

            notificacion.ContribuyenteId = dto.ContribuyenteId;
            notificacion.Tipo = dto.Tipo;
            notificacion.MedioEnvio = dto.MedioEnvio;
            notificacion.Titulo = dto.Titulo;
            notificacion.Mensaje = dto.Mensaje;
            notificacion.Estado = dto.Estado;

            _context.Notificaciones.Update(notificacion);
            await _context.SaveChangesAsync();

            return MapToResponseDto(notificacion);
        }

        public async Task<bool> DeleteNotificacionAsync(int id)
        {
            var notificacion = await _context.Notificaciones.FindAsync(id);
            if (notificacion == null) return false;

            _context.Notificaciones.Remove(notificacion);
            await _context.SaveChangesAsync();
            return true;
        }

        private static NotificacionResponseDto MapToResponseDto(Notificacion notificacion)
        {
            return new NotificacionResponseDto
            {
                Id = notificacion.Id,
                ContribuyenteId = notificacion.ContribuyenteId,
                Tipo = notificacion.Tipo,
                MedioEnvio = notificacion.MedioEnvio,
                Titulo = notificacion.Titulo,
                Mensaje = notificacion.Mensaje,
                FechaEnvio = notificacion.FechaEnvio,
                Estado = notificacion.Estado,
                ContribuyenteNombre = notificacion.Contribuyente?.NombreCompletoRazonSocial ?? "Desconocido"
            };
        }
    }
}
