using backend.dto;

namespace backend.Interfaces
{
    public interface IConfiguracionService
    {
        Task<ConfiguracionDto> GetConfiguracionAsync();
        Task<ConfiguracionDto> UpdateConfiguracionAsync(UpdateConfiguracionDto dto);
    }
}
