using Event;
using NServiceBus;
using NServiceBus.Logging;

namespace MarketplaceService
{
    public class Program
    {
        static ILog log = LogManager.GetLogger<Program>();

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        }).UseNServiceBus(x =>
        {
            //define all the settings that determine how our endpoint will operate.
            var endpointConfiguration = new EndpointConfiguration("MarketplaceService");
            endpointConfiguration.Recoverability().CustomPolicy((config, context) =>
            {
                if (context.Exception is InvalidOperationException invalidOperationException &&
                    invalidOperationException.Message.StartsWith("No handlers could be found", StringComparison.OrdinalIgnoreCase))
                {
                    return RecoverabilityAction.Discard("Callback no longer active");
                }
                return DefaultRecoverabilityPolicy.Invoke(config, context);
            });

            //This setting defines the transport that NServiceBus will use to send and receive messages.
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();

            transport.ConnectionString("host=rabbitmq");

            //Returns a RoutingSettings<RabbitMQTransport>
            var routing = transport.UseConventionalRoutingTopology().Routing();

            // Specify the routing for a specific type - for commands
            routing.RouteToEndpoint(typeof(PlaceOrder), "OrderService");
            routing.RouteToEndpoint(typeof(GetOrderStatus), "OrderService");
            endpointConfiguration.MakeInstanceUniquelyAddressable("OrderService");

            endpointConfiguration.EnableCallbacks();
            endpointConfiguration.EnableInstallers();

            return endpointConfiguration;
        });

    }
}
