namespace backend.Models
{
    public class Contribuyente
    {
        public int Id { get; set; }
        public string TipoDocumento { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
        public string NombreCompletoRazonSocial { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Estado { get; set; } = true;

        public ICollection<Predio>? Predios { get; set; }
        public ICollection<Deuda>? Deudas { get; set; }
        public ICollection<LicenciaTasa>? LicenciasTasas { get; set; }
        public ICollection<Notificacion>? Notificaciones { get; set; }

        public ICollection<PredioContribuyente> PrediosContribuyentes { get; set; } = new List<PredioContribuyente>();
        public ICollection<Obligacion> Obligaciones { get; set; } = new List<Obligacion>();
    }
}
