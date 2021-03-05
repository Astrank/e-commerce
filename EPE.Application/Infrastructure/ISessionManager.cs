using EPE.Domain.Models;
using System.Collections.Generic;

namespace EPE.Application.Infrastructure
{
    public interface ISessionManager
    {
        string GetId();
        void AddProduct(int stockId, int qty);
        void RemoveProduct(int stockId, int qty);
        void DeleteAllFromCart(int stockId);
        List<CartProduct> GetCart();
        void AddCustomerInformation(CustomerInformation customer);
        CustomerInformation GetCustomerInformation();
    }
}
