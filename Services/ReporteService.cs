using backend.Data;
using backend.dto;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class ReporteService : IReporteService
    {
        private readonly AppDbContext _context;

        public ReporteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardMetricsDto> GetDashboardMetricsAsync()
        {
            var totalContribuyentes = await _context.Contribuyentes.CountAsync(c => c.Estado);
            var totalPredios = await _context.Predios.CountAsync();
            var totalRecaudado = await _context.Pagos.SumAsync(p => p.MontoPagado);
            
            // Deuda por cobrar: sumamos el TotalPagar de todas las deudas que estén Pendientes o Vencidas
            var deudaPorCobrar = await _context.Deudas
                .Where(d => d.Estado == "Pendiente" || d.Estado == "Vencida")
                .SumAsync(d => d.TotalPagar);

            var licenciasActivas = await _context.LicenciasTasas.CountAsync(l => l.Estado == "Activa");

            return new DashboardMetricsDto
            {
                TotalContribuyentes = totalContribuyentes,
                TotalPredios = totalPredios,
                TotalRecaudado = totalRecaudado,
                DeudaPorCobrar = deudaPorCobrar,
                LicenciasActivas = licenciasActivas
            };
        }

        public async Task<IEnumerable<ReporteGeneradoDto>> GetHistorialReportesAsync()
        {
            var reportes = await _context.ReportesGenerados
                .OrderByDescending(r => r.FechaGeneracion)
                .ToListAsync();

            return reportes.Select(r => new ReporteGeneradoDto
            {
                Id = r.Id,
                TipoReporte = r.TipoReporte,
                FechaGeneracion = r.FechaGeneracion,
                Usuario = r.Usuario,
                Filtros = r.Filtros,
                TotalRegistros = r.TotalRegistros
            });
        }

        public async Task<ReporteGeneradoDto> RegistrarGeneracionReporteAsync(CreateReporteGeneradoDto dto)
        {
            var reporte = new ReporteGenerado
            {
                TipoReporte = dto.TipoReporte,
                FechaGeneracion = DateTime.Now,
                Usuario = "Admin", // Esto podría venir del token JWT en un sistema real
                Filtros = dto.Filtros,
                TotalRegistros = dto.TotalRegistros
            };

            _context.ReportesGenerados.Add(reporte);
            await _context.SaveChangesAsync();

            return new ReporteGeneradoDto
            {
                Id = reporte.Id,
                TipoReporte = reporte.TipoReporte,
                FechaGeneracion = reporte.FechaGeneracion,
                Usuario = reporte.Usuario,
                Filtros = reporte.Filtros,
                TotalRegistros = reporte.TotalRegistros
            };
        }

        public async Task<IEnumerable<MonthlyRevenueDto>> GetMonthlyRevenueChartDataAsync()
        {
            var data = new List<MonthlyRevenueDto>();
            var startDate = DateTime.Now.AddMonths(-5);
            var startMonth = new DateTime(startDate.Year, startDate.Month, 1);

            // Traer todos los pagos de los últimos 6 meses
            var pagos = await _context.Pagos
                .Where(p => p.FechaPago >= startMonth)
                .ToListAsync();

            // Traer deudas no pagadas o vencidas generadas en los últimos 6 meses (simplificado)
            var deudas = await _context.Deudas
                .Where(d => d.FechaVencimiento >= startMonth && d.Estado != "Pagada")
                .ToListAsync();

            // Agrupar por mes
            for (int i = 0; i < 6; i++)
            {
                var monthDate = startMonth.AddMonths(i);
                var monthName = monthDate.ToString("MMM", new System.Globalization.CultureInfo("es-ES")).ToUpper();

                var recaudadoMes = pagos
                    .Where(p => p.FechaPago.Year == monthDate.Year && p.FechaPago.Month == monthDate.Month)
                    .Sum(p => p.MontoPagado);

                var deudaMes = deudas
                    .Where(d => d.FechaVencimiento.Year == monthDate.Year && d.FechaVencimiento.Month == monthDate.Month)
                    .Sum(d => d.TotalPagar);

                data.Add(new MonthlyRevenueDto
                {
                    Mes = monthName,
                    Recaudado = recaudadoMes,
                    Deuda = deudaMes
                });
            }

            return data;
        }

        public async Task<IEnumerable<RevenueTypeDto>> GetRevenueByTypeChartDataAsync()
        {
            // Para simplificar, calculamos de todas las deudas del año actual
            var currentYear = DateTime.Now.Year;

            var deudas = await _context.Deudas
                .Where(d => d.FechaVencimiento.Year == currentYear)
                .ToListAsync();

            var totalPredial = deudas.Where(d => d.Concepto.Contains("Predial")).Sum(d => d.TotalPagar);
            var totalArbitrios = deudas.Where(d => d.Concepto.Contains("Arbitrio")).Sum(d => d.TotalPagar);
            var totalLicencias = deudas.Where(d => d.Concepto.Contains("Licencia") || d.Concepto.Contains("Tasa")).Sum(d => d.TotalPagar);

            return new List<RevenueTypeDto>
            {
                new RevenueTypeDto { Tipo = "Imp. Predial", Monto = totalPredial, Fill = "#3b82f6" }, // Blue
                new RevenueTypeDto { Tipo = "Arbitrios", Monto = totalArbitrios, Fill = "#10b981" }, // Emerald
                new RevenueTypeDto { Tipo = "Licencias", Monto = totalLicencias, Fill = "#f59e0b" } // Amber
            };
        }
    }
}
