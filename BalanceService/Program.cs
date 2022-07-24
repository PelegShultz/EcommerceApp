using NServiceBus;

namespace BalanceService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Balance";
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
                    var endpointConfiguration = new EndpointConfiguration("BalanceService");

            //This setting defines the transport that NServiceBus will use to send and receive messages.
                    var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();


                    transport.ConnectionString("host=rabbitmq");

            //Returns a RoutingSettings<RabbitMQTransport>
                    var routing = transport.UseConventionalRoutingTopology().Routing();


                    endpointConfiguration.EnableInstallers();

                    return endpointConfiguration;
                });
    }
  
}
