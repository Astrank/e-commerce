using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace EPE.UI.Infrastructure
{
    public class FileManager : IFileManager
    {
        private IConfiguration _config;

        public FileManager(IConfiguration config)
        {
            _config = config;
        }
        
        public void DeleteImage(string rootPath, string image)
        {
            var imgPath = $"{_config[rootPath]}/{image}";
            if (File.Exists(imgPath))
            {
                File.Delete(imgPath);
            }
        }

        public async Task<string> SaveImage(string rootPath, IFormFile image)
        {
            try
            {
                var savePath = Path.Combine(_config[rootPath]);

                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                var mime = image.FileName.Substring(image.FileName.LastIndexOf("."));
                var fileName = $"img_{DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss-fff")}{mime}";

                using(var fileStream = new FileStream(Path.Combine(savePath, fileName), FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                return fileName;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "Error";
            }
        }
    }
}