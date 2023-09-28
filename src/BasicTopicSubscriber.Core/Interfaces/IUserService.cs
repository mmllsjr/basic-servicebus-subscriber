using BasicTopicSubscriber.Core.Entities;

namespace BasicTopicSubscriber.Core.Interfaces
{
    public interface IUserService
    {
        Task ApplySomethingAndSendEmailAsync(User user);
    }
}
