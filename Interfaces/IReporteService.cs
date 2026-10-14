using backend.dto;

namespace backend.Interfaces
{
    public interface IReporteService
    {
        Task<DashboardMetricsDto> GetDashboardMetricsAsync();
        Task<IEnumerable<ReporteGeneradoDto>> GetHistorialReportesAsync();
        Task<ReporteGeneradoDto> RegistrarGeneracionReporteAsync(CreateReporteGeneradoDto dto);
        Task<IEnumerable<MonthlyRevenueDto>> GetMonthlyRevenueChartDataAsync();
        Task<IEnumerable<RevenueTypeDto>> GetRevenueByTypeChartDataAsync();
    }
}
