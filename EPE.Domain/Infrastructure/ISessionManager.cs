using EPE.Domain.Models;
using System.Collections.Generic;

namespace EPE.Domain.Infrastructure
{
    public interface ISessionManager
    {
        string GetId();
        IEnumerable<CartProduct> GetCart();
        void AddProduct(CartProduct cartProduct);
        void RemoveProduct(int stockId, int qty);
        void DeleteAllFromCart(int stockId);
        void ClearCart();
        
        void AddCustomerInformation(CustomerInformation customer);
        CustomerInformation GetCustomerInformation();
    }
}
