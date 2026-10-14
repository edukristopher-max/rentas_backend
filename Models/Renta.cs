using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Renta
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Cliente { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Monto { get; set; }

        public DateTime Fecha { get; set; } = DateTime.UtcNow;
    }
}
