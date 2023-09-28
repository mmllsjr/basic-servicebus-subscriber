using BasicTopicSubscriber.Core.DTOs;
using BasicTopicSubscriber.Core.Entities;

namespace BasicTopicSubscriber.Core.Builders
{
    public static class EmailRequestBuilder
    {
        private static readonly string body = "Hi! Thank you for your registration, I love you forever";

        public static EmailRequest BuildForUser(User user)
        {
            var subject = $"{user.Name} - Welcome in!";

            return new EmailRequest(user.Email, subject, body);
        }
    }
}
