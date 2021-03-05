using EPE.Application.AuthorizedUsers;
using EPE.Application.Cart;
using EPE.Application.OrdersAdmin;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection @this)
        {
            @this.AddTransient<AddCustomerInformation>();
            @this.AddTransient<AddToCart>();
            @this.AddTransient<DeleteAllFromCart>();
            @this.AddTransient<GetCart>();
            @this.AddTransient<GetCustomerInformation>();
            @this.AddTransient<EPE.Application.Cart.GetOrder>();
            @this.AddTransient<RemoveFromCart>();

            @this.AddTransient<EPE.Application.OrdersAdmin.GetOrder>();
            @this.AddTransient<GetOrders>();
            @this.AddTransient<UpdateOrder>();

            @this.AddTransient<CreateUser>();
            
            return @this;
        }
    }
}