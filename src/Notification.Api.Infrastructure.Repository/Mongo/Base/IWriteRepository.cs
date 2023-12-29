using MongoDB.Bson;
using Notification.Api.Domain.Base;

namespace Notification.Api.Infrastructure.Repository.Mongo.Base
{
    public interface IWriteRepository
    {
        Task CreateAsync<TEntity>(TEntity obj, CancellationToken cancellationToken);
        Task CreateManyAsync<TEntity>(IEnumerable<TEntity> objs, CancellationToken cancellationToken);
        Task UpsertAsync<TEntity>(TEntity replacement, CancellationToken cancellationToken) where TEntity : IIdentifier;
        Task DeleteAsync<TEntity>(Guid id, CancellationToken cancellationToken) where TEntity : IIdentifier;
    }
}