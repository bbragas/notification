﻿namespace Notification.Api.Infrastructure.Repository.Mongo.Configuration;

public class MongoSettings
{
    public string ConnectionString { get; set; } = string.Empty;

    public string DatabaseName { get; set; } = string.Empty;
}
