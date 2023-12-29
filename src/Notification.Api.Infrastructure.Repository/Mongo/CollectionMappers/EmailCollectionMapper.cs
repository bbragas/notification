using MongoDB.Bson.Serialization;
using Notification.Api.Domain;

namespace Notification.Api.Infrastructure.Repository.Mongo.CollectionMappers
{
    public class EmailCollectionMapper : ICollectionMapper
    {
        public void Map()
        {
            BsonClassMap.RegisterClassMap<Email>(cm =>
            {
                cm.AutoMap();
            });
        }
    }
}
