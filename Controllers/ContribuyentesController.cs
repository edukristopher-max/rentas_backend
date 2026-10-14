using backend.dto;
using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContribuyentesController : ControllerBase
    {
        private readonly IContribuyenteService _contribuyenteService;

        public ContribuyentesController(IContribuyenteService contribuyenteService)
        {
            _contribuyenteService = contribuyenteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetContribuyentes()
        {
            var contribuyentes = await _contribuyenteService.GetContribuyentesAsync();
            return Ok(contribuyentes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContribuyente(int id)
        {
            var contribuyente = await _contribuyenteService.GetContribuyenteByIdAsync(id);

            if (contribuyente == null)
            {
                return NotFound(new { message = "Contribuyente no encontrado." });
            }

            return Ok(contribuyente);
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarContribuyente([FromQuery] string dni)
        {
            var contribuyente = await _contribuyenteService.GetContribuyenteByDocumentoAsync(dni);

            if (contribuyente == null)
            {
                return NotFound(new { message = "Contribuyente no encontrado." });
            }

            return Ok(contribuyente);
        }

        [HttpPost]
        public async Task<IActionResult> PostContribuyente(CreateContribuyenteDto dto)
        {
            var contribuyente = await _contribuyenteService.CreateContribuyenteAsync(dto);
            return CreatedAtAction(nameof(GetContribuyente), new { id = contribuyente.Id }, contribuyente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutContribuyente(int id, UpdateContribuyenteDto dto)
        {
            var contribuyente = await _contribuyenteService.UpdateContribuyenteAsync(id, dto);

            if (contribuyente == null)
            {
                return NotFound(new { message = "Contribuyente no encontrado." });
            }

            return Ok(contribuyente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContribuyente(int id)
        {
            var result = await _contribuyenteService.DeleteContribuyenteAsync(id);

            if (!result)
            {
                return NotFound(new { message = "Contribuyente no encontrado." });
            }

            return NoContent();
        }
    }
}
