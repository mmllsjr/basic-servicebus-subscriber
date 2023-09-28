using BasicTopicSubscriber.Subscribers;

namespace BasicTopicSubscriber.Installers
{
    public static class SubscribersInstaller
    {
        public static IServiceCollection AddSubscribers(this IServiceCollection services)
        {
            services.AddSingleton<UserCreatedSubscriber>();

            return services;
        }

        public static async Task StartSubscribersAsync(this IApplicationBuilder builder)
        {
            await builder.ApplicationServices.GetRequiredService<UserCreatedSubscriber>().StartAsync(default);
        }
    }
}
