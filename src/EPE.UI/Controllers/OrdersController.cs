using System.Threading.Tasks;
using EPE.Application.OrdersAdmin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EPE.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class OrdersController : Controller
    {
        [HttpGet("")]
        public IActionResult GetOrders([FromServices] GetOrders getOrders,int status) => 
            Ok(getOrders.Do(status));

        [HttpGet("{id}")]
        public IActionResult GetOrder([FromServices] GetOrder getOrder, int id) => 
            Ok(getOrder.Do(id));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder([FromServices] UpdateOrder updateOrder, int id)
        {
            var success = await updateOrder.Do(id) > 0;
                
            if (success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}