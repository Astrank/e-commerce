using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPE.Database;
using EPE.Database.FileManager;

namespace EPE.Application.ProductsAdmin
{
    public class DeleteProduct
    {
        private ApplicationDbContext _context;
        private IFileManager _fileManager;

        public DeleteProduct(ApplicationDbContext context, IFileManager fileManager)
        {
            _context = context;
            _fileManager = fileManager;
        }

        public string rootPath = "ProductsPath:Images";

        public async Task<bool> Do(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);

            _context.Products.Remove(product);

            if (product.Image != null)
            {
                _fileManager.DeleteImage(rootPath , product.Image);
            }

            await _context.SaveChangesAsync();

            return true;
        }
    }
}