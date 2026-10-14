using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class CajaCierre
    {
        public int Id { get; set; }
        public int CajaAperturaId { get; set; } 
        public DateTime FechaCierre { get; set; } = DateTime.UtcNow;
        
        [Column(TypeName = "decimal(18,2)")] public decimal MontoSistema { get; set; } 
        [Column(TypeName = "decimal(18,2)")] public decimal MontoDeclarado { get; set; } 
        [Column(TypeName = "decimal(18,2)")] public decimal Diferencia { get; set; } 
        
        public int UsuarioResponsableId { get; set; }
        [MaxLength(255)] public string Observaciones { get; set; } = string.Empty;

        public CajaApertura? Apertura { get; set; }
    }
}
