using OrderService.Data;
using Microsoft.EntityFrameworkCore;

namespace OrderService.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddDBContext(this IServiceCollection services, IConfiguration config)
        {
            var host = config["DBHOST"] ?? "localhost";
            var password = config["DBPASSWORD"] ?? "secret";

            string myConnection = $"server={host}; userid=root; pwd={password};" +
                    $"port=3306; database=OrdersDB;" + "SSL Mode = None";

            services.AddDbContext<DataContext>(options =>
            {
                options.UseMySql(myConnection, ServerVersion.AutoDetect(myConnection));
            });

            return services;
        }
    }
}
