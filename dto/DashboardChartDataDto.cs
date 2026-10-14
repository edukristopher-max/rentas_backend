namespace backend.dto
{
    public class MonthlyRevenueDto
    {
        public string Mes { get; set; } = string.Empty;
        public decimal Recaudado { get; set; }
        public decimal Deuda { get; set; }
    }

    public class RevenueTypeDto
    {
        public string Tipo { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public string Fill { get; set; } = string.Empty; // Color code for chart
    }
}
