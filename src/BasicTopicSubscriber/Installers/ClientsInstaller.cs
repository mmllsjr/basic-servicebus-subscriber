using Azure.Messaging.ServiceBus;
using System.Net.Http.Headers;

namespace BasicTopicSubscriber.Installers
{
    public static class ClientsInstaller
    {
        public static IServiceCollection AddClients(this IServiceCollection services, IConfiguration configuration)
        {
            var serviceBusConnectionString = configuration.GetConnectionString("ServiceBus");

            services.AddSingleton(sp => new ServiceBusClient(serviceBusConnectionString));

            services.AddHttpClient("external-service", client =>
            {
                client.BaseAddress = new Uri(configuration["ExternalService:BaseUrl"]);

                // if you need, add this
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // and this
                var token = configuration["ExternalService:Token"];

                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            });

            return services;
        }
    }
}
