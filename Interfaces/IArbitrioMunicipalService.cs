using backend.dto;

namespace backend.Interfaces
{
    public interface IArbitrioMunicipalService
    {
        Task<IEnumerable<ArbitrioMunicipalResponseDto>> GetArbitriosAsync();
        Task<ArbitrioMunicipalResponseDto?> GetArbitrioByIdAsync(int id);
        Task<ArbitrioMunicipalResponseDto> CreateArbitrioAsync(CreateArbitrioMunicipalDto dto);
        Task<ArbitrioMunicipalResponseDto?> UpdateArbitrioAsync(int id, UpdateArbitrioMunicipalDto dto);
        Task<bool> DeleteArbitrioAsync(int id);
    }
}
