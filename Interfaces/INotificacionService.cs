using backend.dto;

namespace backend.Interfaces
{
    public interface INotificacionService
    {
        Task<IEnumerable<NotificacionResponseDto>> GetNotificacionesAsync();
        Task<NotificacionResponseDto?> GetNotificacionByIdAsync(int id);
        Task<NotificacionResponseDto> CreateNotificacionAsync(CreateNotificacionDto dto);
        Task<NotificacionResponseDto?> UpdateNotificacionAsync(int id, UpdateNotificacionDto dto);
        Task<bool> DeleteNotificacionAsync(int id);
    }
}
