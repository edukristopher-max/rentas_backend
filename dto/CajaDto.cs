using System;
using System.ComponentModel.DataAnnotations;

namespace backend.dto
{
    public class AbrirCajaDto
    {
        [Required]
        public int UsuarioId { get; set; }
        
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El monto de apertura debe ser mayor o igual a 0.")]
        public decimal MontoApertura { get; set; }
    }

    public class RegistrarMovimientoDto
    {
        [Required]
        public int CajaAperturaId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0.")]
        public decimal Monto { get; set; }

        [Required]
        [RegularExpression("^(Ingreso|Egreso)$", ErrorMessage = "El tipo de movimiento solo puede ser 'Ingreso' o 'Egreso'.")]
        [MaxLength(50)]
        public string Tipo { get; set; } = "Ingreso";

        [Required]
        [RegularExpression("^(Efectivo|Tarjeta de Crédito|Transferencia|Yape/Plin)$", ErrorMessage = "El método de pago no es válido.")]
        [MaxLength(50)]
        public string MetodoPago { get; set; } = "Efectivo";

        public int? PagoId { get; set; }
    }

    public class CerrarCajaDto
    {
        [Required]
        public int CajaAperturaId { get; set; }

        [Required]
        public int UsuarioResponsableId { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El monto declarado debe ser mayor o igual a 0.")]
        public decimal MontoDeclarado { get; set; }

        [MaxLength(255)]
        public string Observaciones { get; set; } = string.Empty;
    }

    public class CajaAperturaResponseDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaApertura { get; set; }
        public decimal MontoApertura { get; set; }
        public string Estado { get; set; } = string.Empty;
    }

    public class CajaMovimientoResponseDto
    {
        public int Id { get; set; }
        public int CajaAperturaId { get; set; }
        public int? PagoId { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public DateTime FechaHora { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
    }

    public class CajaCierreResponseDto
    {
        public int Id { get; set; }
        public int CajaAperturaId { get; set; }
        public DateTime FechaCierre { get; set; }
        public decimal MontoSistema { get; set; }
        public decimal MontoDeclarado { get; set; }
        public decimal Diferencia { get; set; }
        public int UsuarioResponsableId { get; set; }
        public string Observaciones { get; set; } = string.Empty;
    }
}
