using System.Threading.Tasks;
using EPE.Domain.Infrastructure;

namespace EPE.Application.Cart
{
    [Service]
    public class DeleteAllFromCart
    {
        private ISessionManager _sessionManager;
        private IStockManager _stockManager;

        public DeleteAllFromCart(ISessionManager sessionManager, IStockManager stockManager)
        {
            _sessionManager = sessionManager;
            _stockManager = stockManager;
        }

        public class Request
        {
            public int StockId { get; set; }
        }

        public async Task<bool> Do(Request request)
        {
            await _stockManager.DeleteAllFromHold(request.StockId, _sessionManager.GetId());

            _sessionManager.DeleteAllFromCart(request.StockId);

            return true;
        }
    }
}