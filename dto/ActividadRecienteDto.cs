namespace backend.dto
{
    public class ActividadRecienteDto
    {
        public int Id { get; set; }
        public string Modulo { get; set; } = string.Empty;
        public string Accion { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string Usuario { get; set; } = string.Empty;
    }

    public class CreateActividadRecienteDto
    {
        public string Modulo { get; set; } = string.Empty;
        public string Accion { get; set; } = string.Empty;
    }
}
