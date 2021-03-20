using EPE.Application;
using EPE.Database;
using EPE.Domain.Infrastructure;
using EPE.UI.Infrastructure;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceRegister
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection @this)
        {
            var serviceType = typeof(Service);
            var definedTypes = serviceType.Assembly.DefinedTypes;

            var services = definedTypes
                .Where(x => x.GetTypeInfo().GetCustomAttribute<Service>() != null);

            foreach (var service in services)
            {
                @this.AddTransient(service);
            }

            @this.AddTransient<IOrderManager, OrderManager>();
            @this.AddTransient<IProductManager, ProductManager>();
            @this.AddTransient<IProjectManager, ProjectManager>();
            @this.AddTransient<IStockManager, StockManager>();
            @this.AddTransient<ICategoryManager, CategoryManager>();
            
            @this.AddScoped<ISessionManager, SessionManager>();
            
            return @this;
        }
    }
}