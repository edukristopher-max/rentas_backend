using backend.dto;
using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImpuestosPredialesController : ControllerBase
    {
        private readonly IImpuestoPredialService _impuestoService;

        public ImpuestosPredialesController(IImpuestoPredialService impuestoService)
        {
            _impuestoService = impuestoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetImpuestos()
        {
            var impuestos = await _impuestoService.GetImpuestosAsync();
            return Ok(impuestos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImpuesto(int id)
        {
            var impuesto = await _impuestoService.GetImpuestoByIdAsync(id);

            if (impuesto == null)
            {
                return NotFound(new { message = "Impuesto Predial no encontrado." });
            }

            return Ok(impuesto);
        }

        [HttpPost]
        public async Task<IActionResult> PostImpuesto(CreateImpuestoPredialDto dto)
        {
            var impuesto = await _impuestoService.CreateImpuestoAsync(dto);
            return CreatedAtAction(nameof(GetImpuesto), new { id = impuesto.Id }, impuesto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutImpuesto(int id, UpdateImpuestoPredialDto dto)
        {
            var impuesto = await _impuestoService.UpdateImpuestoAsync(id, dto);

            if (impuesto == null)
            {
                return NotFound(new { message = "Impuesto Predial no encontrado." });
            }

            return Ok(impuesto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImpuesto(int id)
        {
            var result = await _impuestoService.DeleteImpuestoAsync(id);

            if (!result)
            {
                return NotFound(new { message = "Impuesto Predial no encontrado." });
            }

            return NoContent();
        }
    }
}
