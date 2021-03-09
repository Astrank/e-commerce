using System.Threading.Tasks;
using EPE.Application.StockAdmin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EPE.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class StocksController : Controller
    {
        [HttpGet("{id}")]
        public IActionResult GetStock([FromServices] GetStock getStock, int id) => 
            Ok(getStock.Do(id));

        [HttpPost("")]
        public async Task<IActionResult> CreateStock(
            [FromBody] CreateStock.Request request,
            [FromServices] CreateStock createStock) => 
                Ok((await createStock.Do(request)));

        [HttpPut("")]
        public async Task<IActionResult> UpdateStock(
            [FromBody] UpdateStock.Request request, 
            [FromServices] UpdateStock updateStock) => 
                Ok(await updateStock.Do(request));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock([FromServices] DeleteStock deleteStock, int id) =>
            Ok(await deleteStock.Do(id));
    }
}