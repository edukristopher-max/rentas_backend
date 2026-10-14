using backend.dto;

namespace backend.Interfaces
{
    public interface ILicenciaTasaService
    {
        Task<IEnumerable<LicenciaTasaResponseDto>> GetLicenciasAsync();
        Task<LicenciaTasaResponseDto?> GetLicenciaByIdAsync(int id);
        Task<LicenciaTasaResponseDto> CreateLicenciaAsync(CreateLicenciaTasaDto dto);
        Task<LicenciaTasaResponseDto?> UpdateLicenciaAsync(int id, UpdateLicenciaTasaDto dto);
        Task<bool> DeleteLicenciaAsync(int id);
    }
}
