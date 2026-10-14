using backend.dto;
using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicenciasTasasController : ControllerBase
    {
        private readonly ILicenciaTasaService _licenciaService;

        public LicenciasTasasController(ILicenciaTasaService licenciaService)
        {
            _licenciaService = licenciaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLicencias()
        {
            var licencias = await _licenciaService.GetLicenciasAsync();
            return Ok(licencias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLicencia(int id)
        {
            var licencia = await _licenciaService.GetLicenciaByIdAsync(id);

            if (licencia == null)
            {
                return NotFound(new { message = "Licencia o Tasa no encontrada." });
            }

            return Ok(licencia);
        }

        [HttpPost]
        public async Task<IActionResult> PostLicencia(CreateLicenciaTasaDto dto)
        {
            var licencia = await _licenciaService.CreateLicenciaAsync(dto);
            return CreatedAtAction(nameof(GetLicencia), new { id = licencia.Id }, licencia);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLicencia(int id, UpdateLicenciaTasaDto dto)
        {
            var licencia = await _licenciaService.UpdateLicenciaAsync(id, dto);

            if (licencia == null)
            {
                return NotFound(new { message = "Licencia o Tasa no encontrada." });
            }

            return Ok(licencia);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLicencia(int id)
        {
            var result = await _licenciaService.DeleteLicenciaAsync(id);

            if (!result)
            {
                return NotFound(new { message = "Licencia o Tasa no encontrada." });
            }

            return NoContent();
        }
    }
}
