using EPE.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EPE.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class OrdersController : Controller
    {
        private ApplicationDbContext _context;
        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}