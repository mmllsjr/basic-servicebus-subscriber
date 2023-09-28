using BasicTopicSubscriber.Core.DomainServices;
using BasicTopicSubscriber.Core.ExternalServices;
using BasicTopicSubscriber.Core.Interfaces;

namespace BasicTopicSubscriber.Installers
{
    public static class ServicesInstaller
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddSingleton<IEmailFacade, EmailFacade>()
                           .AddSingleton<IUserService, UserService>();
        }
    }
}
