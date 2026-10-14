using backend.dto;
using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrediosController : ControllerBase
    {
        private readonly IPredioService _predioService;

        public PrediosController(IPredioService predioService)
        {
            _predioService = predioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPredios()
        {
            var predios = await _predioService.GetPrediosAsync();
            return Ok(predios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPredio(int id)
        {
            var predio = await _predioService.GetPredioByIdAsync(id);

            if (predio == null)
            {
                return NotFound(new { message = "Predio no encontrado." });
            }

            return Ok(predio);
        }

        [HttpPost]
        public async Task<IActionResult> PostPredio(CreatePredioDto dto)
        {
            var predio = await _predioService.CreatePredioAsync(dto);
            return CreatedAtAction(nameof(GetPredio), new { id = predio.Id }, predio);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPredio(int id, UpdatePredioDto dto)
        {
            var predio = await _predioService.UpdatePredioAsync(id, dto);

            if (predio == null)
            {
                return NotFound(new { message = "Predio no encontrado." });
            }

            return Ok(predio);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePredio(int id)
        {
            var result = await _predioService.DeletePredioAsync(id);

            if (!result)
            {
                return NotFound(new { message = "Predio no encontrado." });
            }

            return NoContent();
        }
    }
}
