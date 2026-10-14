using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class AuditoriaLog
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        [Required, MaxLength(50)] public string Accion { get; set; } = string.Empty; 
        [Required, MaxLength(100)] public string Entidad { get; set; } = string.Empty; 
        [Required, MaxLength(50)] public string EntidadId { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; } = DateTime.UtcNow;
        
        public string ValoresAnterioresJson { get; set; } = string.Empty; 
        public string ValoresNuevosJson { get; set; } = string.Empty; 
    }
}
