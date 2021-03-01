using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EPE.Database.FileManager
{
    public interface IFileManager
    {
        FileStream ImageStream(string imagePath, string image);
        Task<string> SaveImage(string imagePath, IFormFile image);
        void DeleteImage(string imagePath, string image);
    }
}