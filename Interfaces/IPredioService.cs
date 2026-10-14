using backend.dto;

namespace backend.Interfaces
{
    public interface IPredioService
    {
        Task<IEnumerable<PredioResponseDto>> GetPrediosAsync();
        Task<PredioResponseDto?> GetPredioByIdAsync(int id);
        Task<PredioResponseDto> CreatePredioAsync(CreatePredioDto dto);
        Task<PredioResponseDto?> UpdatePredioAsync(int id, UpdatePredioDto dto);
        Task<bool> DeletePredioAsync(int id);
    }
}
