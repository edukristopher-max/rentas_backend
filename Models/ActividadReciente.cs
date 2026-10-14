namespace backend.Models
{
    public class ActividadReciente
    {
        public int Id { get; set; }
        public string Modulo { get; set; } = string.Empty; // "Pagos", "Contribuyentes", "Deudas", "Notificaciones"
        public string Accion { get; set; } = string.Empty; // Ej: "Se registró un nuevo pago de S/ 150.00"
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Usuario { get; set; } = "Admin";
    }
}
