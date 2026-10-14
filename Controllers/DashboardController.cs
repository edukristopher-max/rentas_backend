using backend.dto;
using backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IActividadRecienteService _actividadService;
        private readonly IReporteService _reporteService;

        public DashboardController(IActividadRecienteService actividadService, IReporteService reporteService)
        {
            _actividadService = actividadService;
            _reporteService = reporteService;
        }

        [HttpGet("metricas")]
        public async Task<IActionResult> GetMetricasResumen()
        {
            // Reutilizamos el servicio de reportes para las métricas
            var metricas = await _reporteService.GetDashboardMetricsAsync();
            return Ok(metricas);
        }

        [HttpGet("actividades")]
        public async Task<IActionResult> GetActividades()
        {
            var actividades = await _actividadService.GetUltimasActividadesAsync();
            return Ok(actividades);
        }

        [HttpPost("actividades")]
        public async Task<IActionResult> PostActividad(CreateActividadRecienteDto dto)
        {
            var actividad = await _actividadService.RegistrarActividadAsync(dto);
            return Ok(actividad);
        }

        [HttpGet("charts/monthly")]
        public async Task<IActionResult> GetMonthlyChart()
        {
            var data = await _reporteService.GetMonthlyRevenueChartDataAsync();
            return Ok(data);
        }

        [HttpGet("charts/types")]
        public async Task<IActionResult> GetTypesChart()
        {
            var data = await _reporteService.GetRevenueByTypeChartDataAsync();
            return Ok(data);
        }
    }
}
