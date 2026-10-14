namespace backend.Models
{
    public class ReporteGenerado
    {
        public int Id { get; set; }
        public string TipoReporte { get; set; } = string.Empty; // "Cierre de Caja Diario", "Listado de Morosos", etc.
        public DateTime FechaGeneracion { get; set; } = DateTime.Now;
        public string Usuario { get; set; } = "Admin";
        public string Filtros { get; set; } = string.Empty;
        public int TotalRegistros { get; set; }
    }
}
