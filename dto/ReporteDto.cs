namespace backend.dto
{
    public class ReporteGeneradoDto
    {
        public int Id { get; set; }
        public string TipoReporte { get; set; } = string.Empty;
        public DateTime FechaGeneracion { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public string Filtros { get; set; } = string.Empty;
        public int TotalRegistros { get; set; }
    }

    public class CreateReporteGeneradoDto
    {
        public string TipoReporte { get; set; } = string.Empty;
        public string Filtros { get; set; } = string.Empty;
        public int TotalRegistros { get; set; }
    }

    public class DashboardMetricsDto
    {
        public int TotalContribuyentes { get; set; }
        public int TotalPredios { get; set; }
        public decimal TotalRecaudado { get; set; }
        public decimal DeudaPorCobrar { get; set; }
        public int LicenciasActivas { get; set; }
    }
}
