using MongoDB.Driver;

namespace Notification.Api.Infrastructure.Repository.Mongo.Context;

public interface IDbContext
{
    IMongoCollection<T> GetCollection<T>();
}
