using MongoDB.Driver;
using Notification.Api.Infrastructure.Repository.Mongo.Context;
using Notification.Api.Domain.Base;
using MongoDB.Bson;

namespace Notification.Api.Infrastructure.Repository.Mongo.Base;

public class WriteRepository : IWriteRepository
{
    protected readonly IDbContext _mongoContext;
    private Dictionary<Type, dynamic> repository = new Dictionary<Type, dynamic>();

    public WriteRepository(IDbContext context)
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

    public Task CreateAsync<TEntity>(TEntity obj, CancellationToken cancellationToken)
    {
        if (obj is null)
            throw new ArgumentNullException($"{typeof(TEntity).Name} object is null");

        return CreateInternalAsync(obj, cancellationToken);
    }

    private async Task CreateInternalAsync<TEntity>(TEntity obj, CancellationToken cancellationToken)
    {
        await GetCollection<TEntity>().InsertOneAsync(obj, cancellationToken: cancellationToken);
    }

    public Task CreateManyAsync<TEntity>(IEnumerable<TEntity> objs, CancellationToken cancellationToken)
    {
        if (objs is null)
            throw new ArgumentNullException($"{typeof(TEntity).Name} object is null");

        return CreateManyInternalAsync(objs, cancellationToken);
    }

    private async Task CreateManyInternalAsync<TEntity>(IEnumerable<TEntity> objs, CancellationToken cancellationToken)
    {
        await GetCollection<TEntity>().InsertManyAsync(objs, cancellationToken: cancellationToken);
    }

    public Task UpsertAsync<TEntity>(TEntity replacement, CancellationToken cancellationToken)
            where TEntity : IIdentifier
            => _mongoContext
                .GetCollection<TEntity>()
                .ReplaceOneAsync(
                    filter: projection => projection.Id.Equals(replacement.Id),
                    replacement: replacement,
                    options: new ReplaceOptions { IsUpsert = true },
                    cancellationToken: cancellationToken);

    public async Task DeleteAsync<TEntity>(Guid id, CancellationToken cancellationToken) where TEntity : IIdentifier
    {
        if (id == default)
            throw new ArgumentNullException($"{typeof(TEntity).Name} object is invalid");

        await GetCollection<TEntity>().DeleteOneAsync(p => p.Id == id, cancellationToken: cancellationToken);
    }
}


