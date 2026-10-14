using backend.dto;
using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArbitriosMunicipalesController : ControllerBase
    {
        private readonly IArbitrioMunicipalService _arbitrioService;

        public ArbitriosMunicipalesController(IArbitrioMunicipalService arbitrioService)
        {
            _arbitrioService = arbitrioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetArbitrios()
        {
            var arbitrios = await _arbitrioService.GetArbitriosAsync();
            return Ok(arbitrios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArbitrio(int id)
        {
            var arbitrio = await _arbitrioService.GetArbitrioByIdAsync(id);

            if (arbitrio == null)
            {
                return NotFound(new { message = "Arbitrio Municipal no encontrado." });
            }

            return Ok(arbitrio);
        }

        [HttpPost]
        public async Task<IActionResult> PostArbitrio(CreateArbitrioMunicipalDto dto)
        {
            var arbitrio = await _arbitrioService.CreateArbitrioAsync(dto);
            return CreatedAtAction(nameof(GetArbitrio), new { id = arbitrio.Id }, arbitrio);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutArbitrio(int id, UpdateArbitrioMunicipalDto dto)
        {
            var arbitrio = await _arbitrioService.UpdateArbitrioAsync(id, dto);

            if (arbitrio == null)
            {
                return NotFound(new { message = "Arbitrio Municipal no encontrado." });
            }

            return Ok(arbitrio);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArbitrio(int id)
        {
            var result = await _arbitrioService.DeleteArbitrioAsync(id);

            if (!result)
            {
                return NotFound(new { message = "Arbitrio Municipal no encontrado." });
            }

            return NoContent();
        }
    }
}
