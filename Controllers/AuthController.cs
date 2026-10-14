using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // Validar que se recibieron los datos
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Buscar el usuario en la base de datos por correo
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == request.Correo);

            if (usuario == null)
            {
                return Unauthorized(new { mensaje = "Correo o contraseña incorrectos." });
            }

            // NOTA: Para producción, ¡NUNCA debes guardar ni comparar contraseñas en texto plano!
            // Debes usar un hash (por ejemplo, con BCrypt o Argon2). 
            // Aquí lo hacemos simple para propósitos de demostración.
            if (usuario.Password != request.Password)
            {
                return Unauthorized(new { mensaje = "Correo o contraseña incorrectos." });
            }

            // Si es correcto, retornamos un éxito (aquí después podrías generar un token JWT o crear una Cookie)
            return Ok(new { 
                mensaje = "Login exitoso", 
                usuario = new 
                { 
                    id = usuario.Id, 
                    nombreUsuario = usuario.NombreUsuario, 
                    correo = usuario.Correo 
                } 
            });
        }
    }
}
