using backend.dto;

namespace backend.Interfaces
{
    public interface IPagoService
    {
        Task<IEnumerable<PagoResponseDto>> GetPagosAsync();
        Task<PagoResponseDto?> GetPagoByIdAsync(int id);
        Task<PagoResponseDto> CreatePagoAsync(CreatePagoDto dto);
        Task<PagoConfirmadoResponseDto> CreatePagoV2Async(CrearPagoV2Dto dto);
    }
}
