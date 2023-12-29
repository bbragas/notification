using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;
using Notification.Api.Infrastructure.Repository.Mongo.Context;
using Notification.Api.Domain.Base;
using Notification.Api.Messages.Abstractions.Pagination;

namespace Notification.Api.Infrastructure.Repository.Mongo.Base;

internal class ReadRepository : IReadRepository
{
    protected readonly IDbContext _mongoContext;
    private Dictionary<Type, dynamic> repository = new Dictionary<Type, dynamic>();

    public ReadRepository(IDbContext context)
    {
        _mongoContext = context;
    }

    protected IMongoCollection<TEntity> GetCollection<TEntity>()
    {
        if (!repository.ContainsKey(typeof(TEntity)))
        {
            repository.Add(typeof(TEntity), _mongoContext.GetCollection<TEntity>());
        }

        return repository[typeof(TEntity)];
    }

    public Task<List<TEntity>> GetAllAsync<TEntity>(CancellationToken cancellationToken)
    {
        return GetCollection<TEntity>().Find(_ => true).ToListAsync(cancellationToken);
    }

    public Task<List<TEntity>> GetByFilterAsync<TEntity>(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
    {
        return GetCollection<TEntity>().Find(filter).ToListAsync(cancellationToken);
    }

    public Task<TEntity> GetSingleOrDefaultByFilterAsync<TEntity>(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
    {
        return GetCollection<TEntity>().Find(filter).SingleOrDefaultAsync(cancellationToken);
    }

    public Task<bool> ExistByFilterAsync<TEntity>(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
    {
        return GetCollection<TEntity>().Find(filter).AnyAsync(cancellationToken);
    }

    public IQueryable<TEntity> Get<TEntity>()
    {
        return GetCollection<TEntity>().AsQueryable();
    }

    public Task<TEntity> GetAsync<TEntity, TId>(TId id, CancellationToken cancellationToken) where TEntity : IIdentifier
            => FindAsync<TEntity>(projection => projection.Id.Equals(id), cancellationToken);

    private Task<TEntity> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) where TEntity : IIdentifier
            => _mongoContext.GetCollection<TEntity>().AsQueryable().Where(predicate).FirstOrDefaultAsync(cancellationToken);

    public Task<IPagedResult<TEntity>> GetAllDynamicallyAsync<TEntity>(Paging paging, Func<IQueryable<TEntity>, IQueryable<TEntity>> dynamicWhere, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> dynamicOrderBy, CancellationToken cancellationToken)
            where TEntity : IIdentifier
    {
        IQueryable<TEntity> queryable = _mongoContext.GetCollection<TEntity>().AsQueryable();

        queryable = dynamicWhere is null ? queryable : dynamicWhere(queryable);
        queryable = dynamicOrderBy is null ? queryable : dynamicOrderBy(queryable);

        return Pagination.PagedResult<TEntity>.CreateAsync(paging, queryable, cancellationToken);
    }

}


