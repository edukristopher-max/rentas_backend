namespace backend.dto
{
    public class CreatePredioDto
    {
        public string CodigoCatastral { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public decimal AreaTerreno { get; set; }
        public decimal AreaConstruida { get; set; }
        public string UsoPredio { get; set; } = string.Empty;
        public decimal ValorArancelario { get; set; }
        public int ContribuyenteId { get; set; }
    }

    public class UpdatePredioDto : CreatePredioDto
    {
        public bool Estado { get; set; }
    }

    public class PredioResponseDto : UpdatePredioDto
    {
        public int Id { get; set; }
        public string ContribuyenteNombre { get; set; } = string.Empty;
        public string ContribuyenteDocumento { get; set; } = string.Empty;
    }
}
