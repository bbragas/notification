namespace Notification.Api.Messages.BlacklistEntity.DTOs
{
    public record PaginatedDTO<T>(IReadOnlyCollection<T> Items, int Total);
}
