using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Notification.Api.Infrastructure.Repository.Mongo.CollectionMappers;
using Notification.Api.Infrastructure.Repository.Mongo.Configuration;

namespace Notification.Api.Infrastructure.Repository.Mongo.Context;

public class DbContext : IDbContext
{
    private readonly Lazy<IMongoDatabase> _db;

    public DbContext(IOptions<MongoSettings> configuration, IEnumerable<ICollectionMapper> collectionMappers)
    {
        _db = new Lazy<IMongoDatabase>(() =>
        {

            CreateMapper(collectionMappers);
            var mongoClient = new MongoClient(configuration.Value.ConnectionString);
            return mongoClient.GetDatabase(configuration.Value.DatabaseName);
        });
    }

    public void CreateMapper(IEnumerable<ICollectionMapper> collectionMappers)
    {
        foreach (var collectionMapper in collectionMappers)
        {
            collectionMapper.Map();
        }
    }
    public IMongoCollection<T> GetCollection<T>()
            => _db.Value.GetCollection<T>(typeof(T).Name);
}
