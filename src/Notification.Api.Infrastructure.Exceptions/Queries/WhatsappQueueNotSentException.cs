namespace Notification.Api.Infrastructure.Exceptions.Queries
{
    public class WhatsappQueueNotSentException : Exception
    {
        public WhatsappQueueNotSentException() : base("Error when try to get not sent Whatsapp messages count from provider queue.")
        {
        }
    }
}
