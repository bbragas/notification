using Notification.Api.Domain.Base;

namespace Notification.Api.Domain
{
    public class BlacklistEntity : IIdentifier
    {
        public Guid Id { get; init; }
        public Guid ProjectId { get; init; }
        public Guid? ProjectEntityId { get; init; } = null;
        public string Contact { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public DateTime CreatedAt => DateTime.UtcNow;
    }
}
