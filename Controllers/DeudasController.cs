using backend.dto;
using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeudasController : ControllerBase
    {
        private readonly IDeudaService _deudaService;

        public DeudasController(IDeudaService deudaService)
        {
            _deudaService = deudaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDeudas()
        {
            var deudas = await _deudaService.GetDeudasAsync();
            return Ok(deudas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeuda(int id)
        {
            var deuda = await _deudaService.GetDeudaByIdAsync(id);

            if (deuda == null)
            {
                return NotFound(new { message = "Deuda no encontrada." });
            }

            return Ok(deuda);
        }

        [HttpPost]
        public async Task<IActionResult> PostDeuda(CreateDeudaDto dto)
        {
            var deuda = await _deudaService.CreateDeudaAsync(dto);
            return CreatedAtAction(nameof(GetDeuda), new { id = deuda.Id }, deuda);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeuda(int id, UpdateDeudaDto dto)
        {
            var deuda = await _deudaService.UpdateDeudaAsync(id, dto);

            if (deuda == null)
            {
                return NotFound(new { message = "Deuda no encontrada." });
            }

            return Ok(deuda);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeuda(int id)
        {
            var result = await _deudaService.DeleteDeudaAsync(id);

            if (!result)
            {
                return NotFound(new { message = "Deuda no encontrada." });
            }

            return NoContent();
        }
    }
}
