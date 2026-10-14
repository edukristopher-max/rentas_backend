using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class ConfiguracionSistema
    {
        public int Id { get; set; }
        public string NombreMunicipalidad { get; set; } = string.Empty;
        public string Ruc { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string EmailContacto { get; set; } = string.Empty;
        
        // Parámetros Tributarios
        [Column(TypeName = "decimal(5,2)")] public decimal TasaInteresMoratorio { get; set; } // Porcentaje mensual
        [Column(TypeName = "decimal(18,2)")] public decimal UITActual { get; set; } // Valor de la UIT en moneda local
        public string Moneda { get; set; } = "PEN";
    }
}
