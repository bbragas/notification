using MongoDB.Bson.Serialization;
using Notification.Api.Domain.Whatsapp;

namespace Notification.Api.Infrastructure.Repository.Mongo.CollectionMappers;

public class WhatsappCollectionMapper : ICollectionMapper
{
    public void Map()
    {
        BsonClassMap.RegisterClassMap<Whatsapp>(cm =>
        {
            cm.AutoMap();
            cm.SetIgnoreExtraElements(true);
        });
    }
}
