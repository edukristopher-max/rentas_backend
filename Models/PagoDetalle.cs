using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class PagoDetalle
    {
        public int Id { get; set; }
        public int PagoId { get; set; }
        public int ObligacionId { get; set; }
        [Column(TypeName = "decimal(18,2)")] public decimal Monto { get; set; }

        public Pago? Pago { get; set; }
        public Obligacion? Obligacion { get; set; }
    }
}
