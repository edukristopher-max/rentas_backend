using backend.dto;

namespace backend.Interfaces
{
    public interface IActividadRecienteService
    {
        Task<IEnumerable<ActividadRecienteDto>> GetUltimasActividadesAsync(int limit = 20);
        Task<ActividadRecienteDto> RegistrarActividadAsync(CreateActividadRecienteDto dto);
    }
}
