using backend.dto;

namespace backend.Interfaces
{
    public interface IDeudaService
    {
        Task<IEnumerable<DeudaResponseDto>> GetDeudasAsync();
        Task<DeudaResponseDto?> GetDeudaByIdAsync(int id);
        Task<DeudaResponseDto> CreateDeudaAsync(CreateDeudaDto dto);
        Task<DeudaResponseDto?> UpdateDeudaAsync(int id, UpdateDeudaDto dto);
        Task<bool> DeleteDeudaAsync(int id);
    }
}
