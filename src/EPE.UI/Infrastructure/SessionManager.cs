using EPE.Domain.Infrastructure;
using EPE.Domain.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace EPE.UI.Infrastructure
{
    class SessionManager : ISessionManager
    {
        private const string KeyCart = "cart";
        private const string KeyCustomerInfo = "customer-info";
        private readonly ISession _session;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }

        public string GetId()
        {
            return _session.Id;
        }

        public IEnumerable<CartProduct> GetCart()
        {
            var stringObject = _session.GetString(KeyCart);

            if (string.IsNullOrEmpty(stringObject))
            {
                return new List<CartProduct>();
            }

            var cartList = JsonConvert.DeserializeObject<IEnumerable<CartProduct>>(stringObject);

            return cartList;
        }
        
        public void AddProduct(CartProduct cartProduct)
        {
            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString(KeyCart);

            if (!string.IsNullOrEmpty(stringObject))
            {
                cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);
            }

            if (cartList.Any(x => x.StockId == cartProduct.StockId))
            {
                cartList.Find(x => x.StockId == cartProduct.StockId).Qty += cartProduct.Qty;
            }
            else
            {
                cartList.Add(cartProduct);
            };

            stringObject = JsonConvert.SerializeObject(cartList);

            _session.SetString(KeyCart, stringObject);
        }

        public void RemoveProduct(int stockId, int qty)
        {
            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString(KeyCart);

            if (string.IsNullOrEmpty(stringObject))
            {
                return;
            }

            cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);

            if (!cartList.Any(x => x.StockId == stockId))
            {
                return;
            }

            var product = cartList.Find(x => x.StockId == stockId);

            if (product.Qty > qty)
            {
                product.Qty -= qty;
            }

            stringObject = JsonConvert.SerializeObject(cartList);

            _session.SetString(KeyCart, stringObject);
        }

        public void DeleteAllFromCart(int stockId)
        {
            var cartList = new List<CartProduct>();
            var stringObject = _session.GetString(KeyCart);

            if (string.IsNullOrEmpty(stringObject))
            {
                return;
            }

            cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);

            if (!cartList.Any(x => x.StockId == stockId))
            {
                return;
            }

            var product = cartList.Find(x => x.StockId == stockId);

            cartList.Remove(product);

            stringObject = JsonConvert.SerializeObject(cartList);

            _session.SetString(KeyCart, stringObject);
        }

        public void ClearCart()
        {
            _session.Remove(KeyCart);
        }


        public void AddCustomerInformation(CustomerInformation customer)
        {
            var stringObject = JsonConvert.SerializeObject(customer);

            _session.SetString(KeyCustomerInfo, stringObject);
        }

        public CustomerInformation GetCustomerInformation()
        {
            var stringObject = _session.GetString(KeyCustomerInfo);

            if (string.IsNullOrEmpty(stringObject))
            {
                return null;
            }

            var customerInfo = JsonConvert.DeserializeObject<CustomerInformation>(stringObject);

            return customerInfo;
        }
    }
}
