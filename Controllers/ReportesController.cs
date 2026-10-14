using backend.dto;
using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly IReporteService _reporteService;

        public ReportesController(IReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        [HttpGet("metricas")]
        public async Task<IActionResult> GetMetricas()
        {
            var metricas = await _reporteService.GetDashboardMetricsAsync();
            return Ok(metricas);
        }

        [HttpGet("historial")]
        public async Task<IActionResult> GetHistorial()
        {
            var historial = await _reporteService.GetHistorialReportesAsync();
            return Ok(historial);
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> PostRegistrarReporte(CreateReporteGeneradoDto dto)
        {
            var reporte = await _reporteService.RegistrarGeneracionReporteAsync(dto);
            return Ok(reporte);
        }
    }
}
