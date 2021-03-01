using System.Threading.Tasks;
using EPE.Application.AuthorizedUsers;
using EPE.Application.ProductsAdmin;
using EPE.Application.StockAdmin;
using EPE.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EPE.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Admin")]
    public class UsersController : Controller
    {
        private CreateUser _createUser;
        public UsersController(CreateUser createUser)
        {
            _createUser = createUser;
        }

        public async Task<IActionResult> CreateUser([FromBody] CreateUser.Request request)
        {
            await _createUser.Do(request);

            return Ok();
        }
    }
}