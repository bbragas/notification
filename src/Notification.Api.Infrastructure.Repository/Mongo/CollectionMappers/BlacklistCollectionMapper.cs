using MongoDB.Bson.Serialization;
using Notification.Api.Domain;

namespace Notification.Api.Infrastructure.Repository.Mongo.CollectionMappers
{
    public class BlacklistCollectionMapper : ICollectionMapper
    {
        public void Map()
        {
            BsonClassMap.RegisterClassMap<BlacklistEntity>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}
