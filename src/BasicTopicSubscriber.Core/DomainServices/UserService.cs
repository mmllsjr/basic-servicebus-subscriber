using BasicTopicSubscriber.Core.Builders;
using BasicTopicSubscriber.Core.Entities;
using BasicTopicSubscriber.Core.Exceptions;
using BasicTopicSubscriber.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace BasicTopicSubscriber.Core.DomainServices
{
    // This is an example
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IEmailFacade _emailFacade;

        public UserService(ILogger<UserService> logger, IEmailFacade emailFacade)
        {
            _logger = logger;
            _emailFacade = emailFacade;
        }

        public async Task ApplySomethingAndSendEmailAsync(User user)
        {
            var emailRequest = EmailRequestBuilder.BuildForUser(user);

            var emailResponse = await _emailFacade.SendAsync(emailRequest);

            if (!emailResponse.IsSuccess)
            {
                throw new EmailSendingFailedException($"Couldn't send e-mail to user. Reason: {emailResponse.ErrorMessage}");
            }

            _logger.LogInformation("E-mail created and sent with id {id}", emailResponse.Value.Id);
        }
    }
}
