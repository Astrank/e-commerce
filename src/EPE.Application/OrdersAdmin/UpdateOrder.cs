using System.Threading.Tasks;
using EPE.Domain.Infrastructure;

namespace EPE.Application.OrdersAdmin
{
    [Service]
    public class UpdateOrder
    {
        private readonly IOrderManager _orderManager;

        public UpdateOrder(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        public Task<int> Do(int id)
        {
            return _orderManager.AdvanceOrderStatus(id);
        }
    }
}