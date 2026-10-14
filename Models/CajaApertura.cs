using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class CajaApertura
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaApertura { get; set; } = DateTime.UtcNow;
        [Column(TypeName = "decimal(18,2)")] public decimal MontoApertura { get; set; }
        [MaxLength(50)] public string Estado { get; set; } = "Abierta";

        public ICollection<CajaMovimiento> Movimientos { get; set; } = new List<CajaMovimiento>();
        public CajaCierre? Cierre { get; set; }
    }
}
