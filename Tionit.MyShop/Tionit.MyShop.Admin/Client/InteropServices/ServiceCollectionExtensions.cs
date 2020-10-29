using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Tionit.MyShop.Admin.InteropServices
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInteropServices(this IServiceCollection col)
        {
            col.TryAddScoped<Blocker>();
            col.TryAddScoped<Cookies>();
            col.TryAddScoped<Messages>();
        }
    }
}
