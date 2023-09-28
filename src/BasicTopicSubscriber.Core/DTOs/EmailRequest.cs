namespace BasicTopicSubscriber.Core.DTOs
{
    public record EmailRequest
        (
            string To,
            string Subject,
            string Body
        );
}
