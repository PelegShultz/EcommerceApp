using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Event;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NServiceBus;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .ConfigureLogging(logging => logging.AddConsole())
            .UseNServiceBus(x =>
            {
                //define all the settings that determine how our endpoint will operate.
                var endpointConfiguration = new EndpointConfiguration("API");

                //This setting defines the transport that NServiceBus will use to send and receive messages.
                var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();

                transport.ConnectionString("host=rabbitmq");

                //Returns a RoutingSettings<RabbitMQTransport>
                var routing = transport.UseConventionalRoutingTopology().Routing();

                // Specify the routing for a specific type - for commands
                routing.RouteToEndpoint(typeof(UserCreated), "BalanceService");

                endpointConfiguration.EnableInstallers();

                return endpointConfiguration;
            });
    }
}
