namespace backend.dto
{
    public class CreateLicenciaTasaDto
    {
        public int ContribuyenteId { get; set; }
        public int? PredioId { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string NumeroExpediente { get; set; } = string.Empty;
        public string Concepto { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public string Estado { get; set; } = "En Trámite";
    }

    public class UpdateLicenciaTasaDto : CreateLicenciaTasaDto
    {
    }

    public class LicenciaTasaResponseDto : UpdateLicenciaTasaDto
    {
        public int Id { get; set; }
        public string ContribuyenteNombre { get; set; } = string.Empty;
        public string PredioDireccion { get; set; } = string.Empty;
    }
}
