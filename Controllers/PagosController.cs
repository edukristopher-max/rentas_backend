using backend.dto;
using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly IPagoService _pagoService;

        public PagosController(IPagoService pagoService)
        {
            _pagoService = pagoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPagos()
        {
            var pagos = await _pagoService.GetPagosAsync();
            return Ok(pagos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPago(int id)
        {
            var pago = await _pagoService.GetPagoByIdAsync(id);

            if (pago == null)
            {
                return NotFound(new { message = "Pago no encontrado." });
            }

            return Ok(pago);
        }

        [HttpPost]
        public async Task<IActionResult> PostPago(CreatePagoDto dto)
        {
            try
            {
                var pago = await _pagoService.CreatePagoAsync(dto);
                return CreatedAtAction(nameof(GetPago), new { id = pago.Id }, pago);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("v2")]
        public async Task<IActionResult> PostPagoV2(CrearPagoV2Dto dto)
        {
            try
            {
                var response = await _pagoService.CreatePagoV2Async(dto);
                return Ok(response);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "La obligación ha sido modificada por otra transacción. Por favor, actualice los datos e intente nuevamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
