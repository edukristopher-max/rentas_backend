namespace backend.dto
{
    public class CreateContribuyenteDto
    {
        public string TipoDocumento { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
        public string NombreCompletoRazonSocial { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class UpdateContribuyenteDto : CreateContribuyenteDto
    {
        public bool Estado { get; set; }
    }
}
