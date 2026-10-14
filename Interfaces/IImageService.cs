using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace backend.Interfaces
{
    public interface IImageService
    {
        Task<string> AddImageAsync(IFormFile file);
        Task<bool> DeleteImageAsync(string publicId);
    }
}
