using Azure.Messaging.ServiceBus;
using BasicTopicSubscriber.Core.Entities;
using BasicTopicSubscriber.Core.Interfaces;

namespace BasicTopicSubscriber.Subscribers
{
    // This is an example for implementing BaseSuscriber
    public class UserCreatedSubscriber : BaseSubscriber
    {
        private readonly ILogger<UserCreatedSubscriber> _logger;
        private readonly IUserService _userService;

        public UserCreatedSubscriber(ServiceBusClient serviceBusClient,
            ILogger<UserCreatedSubscriber> logger,
            IUserService userService) : base(serviceBusClient, "your-topic-name", "your-subscription-name")
        {
            _logger = logger;
            _userService = userService;
        }

        public override async Task ProcessMessageAsync(ProcessMessageEventArgs args)
        {
            var user = args.Message.Body.ToObjectFromJson<User>();

            // You could add some null validation

            _logger.LogInformation("Starting some processing for UserId {userId}", user.Id);

            await _userService.ApplySomethingAndSendEmailAsync(user);

            _logger.LogInformation("UserId {userId} processed", user.Id);
        }

        public override Task HandleErrorAsync(ProcessErrorEventArgs args)
        {
            // Add your logic for handling errors

            _logger.LogInformation("Starting some error handling for UserCreatedSubscriber");

            _logger.LogError(args.Exception, "You can log the exception like this. Here's the exception message: {message}", args.Exception.Message);

            return Task.CompletedTask;
        }
    }
}
