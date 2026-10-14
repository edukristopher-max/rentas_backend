using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class PredioContribuyente
    {
        public int Id { get; set; }
        public int PredioId { get; set; }
        public int ContribuyenteId { get; set; }
        
        [Required, MaxLength(50)] public string TipoRelacion { get; set; } = "Propietario";
        [Column(TypeName = "decimal(5,2)")] public decimal PorcentajeParticipacion { get; set; }
        
        public DateTime FechaInicio { get; set; } = DateTime.UtcNow;
        public DateTime? FechaFin { get; set; }
        public bool Estado { get; set; } = true;

        public Predio? Predio { get; set; }
        public Contribuyente? Contribuyente { get; set; }
    }
}
