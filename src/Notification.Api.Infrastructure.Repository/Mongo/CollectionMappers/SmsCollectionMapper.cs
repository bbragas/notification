using MongoDB.Bson.Serialization;
using Notification.Api.Domain;

namespace Notification.Api.Infrastructure.Repository.Mongo.CollectionMappers
{
    public class SmsCollectionMapper : ICollectionMapper
    {
        public void Map()
        {
            BsonClassMap.RegisterClassMap<Sms>(cm =>
            {
                cm.AutoMap();
            });
        }
    }
}
