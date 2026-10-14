using backend.dto;
using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionesController : ControllerBase
    {
        private readonly INotificacionService _notificacionService;

        public NotificacionesController(INotificacionService notificacionService)
        {
            _notificacionService = notificacionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetNotificaciones()
        {
            var notificaciones = await _notificacionService.GetNotificacionesAsync();
            return Ok(notificaciones);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotificacion(int id)
        {
            var notificacion = await _notificacionService.GetNotificacionByIdAsync(id);

            if (notificacion == null)
            {
                return NotFound(new { message = "Notificación no encontrada." });
            }

            return Ok(notificacion);
        }

        [HttpPost]
        public async Task<IActionResult> PostNotificacion(CreateNotificacionDto dto)
        {
            var notificacion = await _notificacionService.CreateNotificacionAsync(dto);
            return CreatedAtAction(nameof(GetNotificacion), new { id = notificacion.Id }, notificacion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotificacion(int id, UpdateNotificacionDto dto)
        {
            var notificacion = await _notificacionService.UpdateNotificacionAsync(id, dto);

            if (notificacion == null)
            {
                return NotFound(new { message = "Notificación no encontrada." });
            }

            return Ok(notificacion);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotificacion(int id)
        {
            var result = await _notificacionService.DeleteNotificacionAsync(id);

            if (!result)
            {
                return NotFound(new { message = "Notificación no encontrada." });
            }

            return NoContent();
        }
    }
}
