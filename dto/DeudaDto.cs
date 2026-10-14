namespace backend.dto
{
    public class CreateDeudaDto
    {
        public int ContribuyenteId { get; set; }
        public int? PredioId { get; set; }
        public string Concepto { get; set; } = string.Empty;
        public string TipoDeuda { get; set; } = string.Empty;
        
        public decimal MontoOriginal { get; set; }
        public decimal Mora { get; set; }
        public decimal Descuento { get; set; }

        public DateTime FechaVencimiento { get; set; }
        public string Estado { get; set; } = "Pendiente";
    }

    public class UpdateDeudaDto : CreateDeudaDto
    {
    }

    public class DeudaResponseDto : UpdateDeudaDto
    {
        public int Id { get; set; }
        public decimal TotalPagar { get; set; }
        public string ContribuyenteNombre { get; set; } = string.Empty;
        public string PredioDireccion { get; set; } = string.Empty;
    }
}
