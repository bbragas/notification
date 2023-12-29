using Notification.Api.Messages.Abstractions.Pagination;

namespace Notification.Api.Messages.Abstractions.Queries.Responses
{
    public abstract record ResponsePagedResult<T> : IResponse, IPagedResult<T>
    {
        public IEnumerable<T> Items { get; init; }
        public int Total { get; init; }
    }
}
