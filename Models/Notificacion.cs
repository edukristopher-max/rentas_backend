using System.Text.Json.Serialization;

namespace backend.Models
{
    public class Notificacion
    {
        public int Id { get; set; }
        public int ContribuyenteId { get; set; }
        public string Tipo { get; set; } = string.Empty; // "Aviso de Cobranza", "Recordatorio de Vencimiento", "Informativa"
        public string MedioEnvio { get; set; } = string.Empty; // "Email", "SMS", "Físico", "WhatsApp"
        public string Titulo { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
        public DateTime FechaEnvio { get; set; } = DateTime.Now;
        public string Estado { get; set; } = "Enviada"; // "Enviada", "Entregada", "Leída", "Fallida"

        public bool Leida { get; set; } = false;
        public DateTime? FechaLectura { get; set; }

        public int? ObligacionId { get; set; }
        public int? PagoId { get; set; }
        public int? LicenciaTasaId { get; set; }
        public int? PredioId { get; set; }

        [JsonIgnore]
        public Contribuyente? Contribuyente { get; set; }
        public Obligacion? Obligacion { get; set; }
        public Pago? Pago { get; set; }
        public LicenciaTasa? LicenciaTasa { get; set; }
        public Predio? Predio { get; set; }
    }
}
