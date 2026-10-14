using backend.dto;
using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CajasController : ControllerBase
    {
        private readonly ICajaService _cajaService;

        public CajasController(ICajaService cajaService)
        {
            _cajaService = cajaService;
        }

        [HttpPost("abrir")]
        public async Task<IActionResult> AbrirCaja([FromBody] AbrirCajaDto dto)
        {
            try
            {
                var result = await _cajaService.AbrirCajaAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("abierta/{usuarioId}")]
        public async Task<IActionResult> ObtenerCajaAbierta(int usuarioId)
        {
            var result = await _cajaService.ObtenerCajaAbiertaAsync(usuarioId);
            if (result == null)
            {
                return NotFound(new { message = "No hay caja abierta para este usuario." });
            }
            return Ok(result);
        }

        [HttpPost("movimiento")]
        public async Task<IActionResult> RegistrarMovimiento([FromBody] RegistrarMovimientoDto dto)
        {
            try
            {
                var result = await _cajaService.RegistrarMovimientoAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{cajaId}/movimientos")]
        public async Task<IActionResult> ObtenerMovimientos(int cajaId)
        {
            var result = await _cajaService.ObtenerMovimientosAsync(cajaId);
            return Ok(result);
        }

        [HttpPost("cerrar")]
        public async Task<IActionResult> CerrarCaja([FromBody] CerrarCajaDto dto)
        {
            try
            {
                var result = await _cajaService.CerrarCajaAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
