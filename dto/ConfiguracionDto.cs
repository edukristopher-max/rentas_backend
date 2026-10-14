namespace backend.dto
{
    public class ConfiguracionDto
    {
        public int Id { get; set; }
        public string NombreMunicipalidad { get; set; } = string.Empty;
        public string Ruc { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string EmailContacto { get; set; } = string.Empty;
        public decimal TasaInteresMoratorio { get; set; }
        public decimal UITActual { get; set; }
        public string Moneda { get; set; } = string.Empty;
    }

    public class UpdateConfiguracionDto
    {
        public string NombreMunicipalidad { get; set; } = string.Empty;
        public string Ruc { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string EmailContacto { get; set; } = string.Empty;
        public decimal TasaInteresMoratorio { get; set; }
        public decimal UITActual { get; set; }
        public string Moneda { get; set; } = string.Empty;
    }
}
