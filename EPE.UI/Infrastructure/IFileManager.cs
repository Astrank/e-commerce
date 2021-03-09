using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace EPE.UI.Infrastructure
{
    public interface IFileManager
    {
        Task<string> SaveImage(string imagePath, IFormFile image);
        void DeleteImage(string imagePath, string image);
    }
}