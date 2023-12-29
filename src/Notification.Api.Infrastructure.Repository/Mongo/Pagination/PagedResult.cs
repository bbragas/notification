using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Notification.Api.Messages.Abstractions.Pagination;

namespace Notification.Api.Infrastructure.Repository.Mongo.Pagination
{
    public class PagedResult<T> : IPagedResult<T>
    {
        private readonly IEnumerable<T> _items;
        private readonly int _perPage;
        private readonly int _totalItems;

        private PagedResult(IEnumerable<T> items, int perPage, int page, int totalItems)
        {
            _items = items;
            _perPage = perPage;
            _totalItems = totalItems;
        }

        public IEnumerable<T> Items
            => _items.Take(_perPage);

        public int Total => _totalItems;

        public static async Task<IPagedResult<T>> CreateAsync(Paging paging, IQueryable<T> source, CancellationToken cancellationToken)
        {
            paging ??= new Paging();
            var totalItems = source.Count();
            var items = await ApplyPagination(paging, source).ToListAsync(cancellationToken);
            return new PagedResult<T>(items, paging.PerPage, paging.Page, totalItems);
        }

        private static IMongoQueryable<T>? ApplyPagination(Paging paging, IQueryable<T> source)
            => source.Skip(paging.PerPage * paging.Page).Take(paging.PerPage + 1) as IMongoQueryable<T>;
    }
}
