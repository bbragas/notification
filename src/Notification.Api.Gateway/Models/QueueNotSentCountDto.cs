namespace Notification.Api.Gateway.Http.Models
{
    public class QueueNotSentCountDto
    {
        public bool Success { get; set; }
        public StatsDto? Stats { get; set; }
    }
    public class StatsDto
    {
        public int Sending { get; set; }
    }
}
