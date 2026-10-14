namespace backend.dto
{
    public class CreateImpuestoPredialDto
    {
        public int PredioId { get; set; }
        public int AnioFiscal { get; set; }
        public decimal MontoBase { get; set; }
        public decimal ImpuestoCalculado { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string EstadoPago { get; set; } = "Pendiente";
    }

    public class UpdateImpuestoPredialDto : CreateImpuestoPredialDto
    {
        public DateTime? FechaPago { get; set; }
    }

    public class ImpuestoPredialResponseDto : UpdateImpuestoPredialDto
    {
        public int Id { get; set; }
        public string PredioCodigoCatastral { get; set; } = string.Empty;
        public string PredioDireccion { get; set; } = string.Empty;
    }
}
