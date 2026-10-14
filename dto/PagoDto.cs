using System.ComponentModel.DataAnnotations;

namespace backend.dto
{
    public class CreatePagoDto
    {
        public int DeudaId { get; set; }
        public decimal MontoPagado { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
        public string Cajero { get; set; } = string.Empty;
        public string Notas { get; set; } = string.Empty;
    }

    public class CrearPagoV2Dto
    {
        [Required] public int UsuarioCajeroId { get; set; }
        [Required] public int CajaAperturaId { get; set; }
        [Required]
        [RegularExpression("^(Efectivo|Tarjeta de Crédito|Transferencia|Yape/Plin)$", ErrorMessage = "El método de pago no es válido.")]
        public string MetodoPago { get; set; } = string.Empty;
        public string Notas { get; set; } = string.Empty;
        
        [Required]
        [MinLength(1, ErrorMessage = "Debe enviar al menos un detalle de pago.")]
        public List<PagoDetalleRequestDto> Detalles { get; set; } = new List<PagoDetalleRequestDto>();
    }

    public class PagoDetalleRequestDto
    {
        [Required] public int ObligacionId { get; set; }
        
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto aplicado debe ser mayor a cero.")]
        public decimal MontoAplicado { get; set; }
    }

    public class PagoConfirmadoResponseDto
    {
        public int PagoId { get; set; }
        public string NumeroRecibo { get; set; } = string.Empty;
        public decimal TotalPagado { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
        public DateTime FechaPago { get; set; }
        public List<string> ObligacionesAfectadas { get; set; } = new List<string>();
        public int CajaMovimientoId { get; set; }
    }

    public class PagoResponseDto
    {
        public int Id { get; set; }
        public int? DeudaId { get; set; }
        public string NumeroRecibo { get; set; } = string.Empty;
        public DateTime FechaPago { get; set; }
        public decimal MontoPagado { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
        public string Cajero { get; set; } = string.Empty;
        public string Notas { get; set; } = string.Empty;
        
        // Datos extraídos de la deuda pagada
        public string DeudaConcepto { get; set; } = string.Empty;
        public string ContribuyenteNombre { get; set; } = string.Empty;
    }
}
