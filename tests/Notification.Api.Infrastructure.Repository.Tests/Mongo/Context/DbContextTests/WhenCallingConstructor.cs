using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using Notification.Api.Infrastructure.Repository.Mongo.CollectionMappers;
using Notification.Api.Infrastructure.Repository.Mongo.Configuration;
using Notification.Api.Infrastructure.Repository.Mongo.Context;
using NSubstitute;

namespace Notification.Api.Infrastructure.Repository.Tests.Mongo.Context.DbContextTests;

[TestClass]
public class WhenCallingConstructor
{
    private readonly Fixture _fixture = new();
    private readonly DbContext _act;

    public WhenCallingConstructor()
    {
        var mongoSettings = new MongoSettings
        {
            ConnectionString = "mongodb://test",
            DatabaseName = _fixture.Create<string>()
        };

        var optionsMongoSettings = Substitute.For<IOptions<MongoSettings>>();
        var mongoClient = Substitute.For<IMongoClient>();

        optionsMongoSettings.Value.Returns(mongoSettings);
        mongoClient.GetDatabase(
            Arg.Is(mongoSettings.DatabaseName),
            Arg.Any<MongoDatabaseSettings>()
            ).Returns(Substitute.For<IMongoDatabase>());

        _act = new DbContext(optionsMongoSettings, Substitute.For<IEnumerable<ICollectionMapper>>());
    }

    [TestMethod]
    public void ShouldNotBeNull()
        => _act.Should().NotBeNull();
}
