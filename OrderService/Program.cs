using Event;
using NServiceBus;
using NServiceBus.Logging;

namespace OrderService
{
    public class Program
    {
        static ILog log = LogManager.GetLogger<Program>();

        public static void Main(string[] args)
        {
            Console.Title = "Order";
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
            var endpointConfiguration = new EndpointConfiguration("OrderService");
            endpointConfiguration.MakeInstanceUniquelyAddressable("MarketplaceService");
            //This setting defines the transport that NServiceBus will use to send and receive messages.
            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            endpointConfiguration.UsePersistence<LearningPersistence>();
            transport.ConnectionString("host=rabbitmq");

            //Returns a RoutingSettings<RabbitMQTransport>
            var routing = transport.UseConventionalRoutingTopology().Routing();
            //routing.RouteToEndpoint(typeof(ReturnToStock), "StockService");
            endpointConfiguration.EnableCallbacks();
            endpointConfiguration.EnableInstallers();

            return endpointConfiguration;
        });

    }
}
