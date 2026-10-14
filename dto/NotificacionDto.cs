namespace backend.dto
{
    public class CreateNotificacionDto
    {
        public int ContribuyenteId { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string MedioEnvio { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
    }

    public class UpdateNotificacionDto : CreateNotificacionDto
    {
        public string Estado { get; set; } = string.Empty;
    }

    public class NotificacionResponseDto : UpdateNotificacionDto
    {
        public int Id { get; set; }
        public DateTime FechaEnvio { get; set; }
        public string ContribuyenteNombre { get; set; } = string.Empty;
    }
}
