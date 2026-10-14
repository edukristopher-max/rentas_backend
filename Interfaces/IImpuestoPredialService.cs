using backend.dto;

namespace backend.Interfaces
{
    public interface IImpuestoPredialService
    {
        Task<IEnumerable<ImpuestoPredialResponseDto>> GetImpuestosAsync();
        Task<ImpuestoPredialResponseDto?> GetImpuestoByIdAsync(int id);
        Task<ImpuestoPredialResponseDto> CreateImpuestoAsync(CreateImpuestoPredialDto dto);
        Task<ImpuestoPredialResponseDto?> UpdateImpuestoAsync(int id, UpdateImpuestoPredialDto dto);
        Task<bool> DeleteImpuestoAsync(int id);
    }
}
