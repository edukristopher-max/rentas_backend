using backend.dto;
using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfiguracionesController : ControllerBase
    {
        private readonly IConfiguracionService _configuracionService;

        public ConfiguracionesController(IConfiguracionService configuracionService)
        {
            _configuracionService = configuracionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetConfiguracion()
        {
            var config = await _configuracionService.GetConfiguracionAsync();
            return Ok(config);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateConfiguracion(UpdateConfiguracionDto dto)
        {
            var config = await _configuracionService.UpdateConfiguracionAsync(dto);
            return Ok(config);
        }
    }
}
