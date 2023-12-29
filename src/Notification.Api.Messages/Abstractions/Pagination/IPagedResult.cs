namespace Notification.Api.Messages.Abstractions.Pagination
{
    public interface IPagedResult<out T>
    {
        IEnumerable<T> Items { get; }
        int Total { get; }
    }
}
