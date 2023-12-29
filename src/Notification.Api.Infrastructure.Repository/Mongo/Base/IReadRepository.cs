using Notification.Api.Domain.Base;
using Notification.Api.Messages.Abstractions.Pagination;
using System.Linq.Expressions;

namespace Notification.Api.Infrastructure.Repository.Mongo.Base
{
    public interface IReadRepository
    {
        Task<bool> ExistByFilterAsync<TEntity>(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken);
        Task<List<TEntity>> GetAllAsync<TEntity>(CancellationToken cancellationToken);
        Task<List<TEntity>> GetByFilterAsync<TEntity>(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken);
        Task<TEntity> GetSingleOrDefaultByFilterAsync<TEntity>(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken);
        IQueryable<TEntity> Get<TEntity>();
        Task<IPagedResult<TEntity>> GetAllDynamicallyAsync<TEntity>(Paging paging, Func<IQueryable<TEntity>, IQueryable<TEntity>> dynamicWhere, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> dynamicOrderBy, CancellationToken cancellationToken)
            where TEntity : IIdentifier;
    }
}