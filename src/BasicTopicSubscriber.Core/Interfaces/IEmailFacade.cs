using BasicTopicSubscriber.Core.DTOs;

namespace BasicTopicSubscriber.Core.Interfaces
{
    public interface IEmailFacade
    {
        Task<Result<EmailResponse>> SendAsync(EmailRequest emailRequest);
    }
}
