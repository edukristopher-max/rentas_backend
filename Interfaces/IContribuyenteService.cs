using backend.Models;
using backend.dto;

namespace backend.Interfaces
{
    public interface IContribuyenteService
    {
        Task<IEnumerable<Contribuyente>> GetContribuyentesAsync();
        Task<Contribuyente?> GetContribuyenteByIdAsync(int id);
        Task<Contribuyente?> GetContribuyenteByDocumentoAsync(string numeroDocumento);
        Task<Contribuyente> CreateContribuyenteAsync(CreateContribuyenteDto createContribuyenteDto);
        Task<Contribuyente?> UpdateContribuyenteAsync(int id, UpdateContribuyenteDto updateContribuyenteDto);
        Task<bool> DeleteContribuyenteAsync(int id);
    }
}
