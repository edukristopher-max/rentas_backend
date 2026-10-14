namespace backend.dto
{
    public class CreateArbitrioMunicipalDto
    {
        public int PredioId { get; set; }
        public int AnioFiscal { get; set; }
        public string Periodo { get; set; } = string.Empty;
        public decimal MontoLimpieza { get; set; }
        public decimal MontoParques { get; set; }
        public decimal MontoSerenazgo { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string EstadoPago { get; set; } = "Pendiente";
    }

    public class UpdateArbitrioMunicipalDto : CreateArbitrioMunicipalDto
    {
        public DateTime? FechaPago { get; set; }
    }

    public class ArbitrioMunicipalResponseDto : UpdateArbitrioMunicipalDto
    {
        public int Id { get; set; }
        public decimal MontoTotal { get; set; }
        public string PredioCodigoCatastral { get; set; } = string.Empty;
        public string PredioDireccion { get; set; } = string.Empty;
    }
}
