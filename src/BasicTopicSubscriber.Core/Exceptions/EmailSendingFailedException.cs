using System.Runtime.Serialization;

namespace BasicTopicSubscriber.Core.Exceptions
{
    [Serializable]
    public class EmailSendingFailedException : Exception
    {
        public EmailSendingFailedException()
        {
        }

        public EmailSendingFailedException(string message) : base(message)
        {
        }

        public EmailSendingFailedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected EmailSendingFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
