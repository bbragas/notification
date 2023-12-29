using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using Notification.Api.Domain;
using Notification.Api.Infrastructure.Repository.Mongo.CollectionMappers;
using Notification.Api.Infrastructure.Repository.Mongo.Configuration;
using Notification.Api.Infrastructure.Repository.Mongo.Context;
using NSubstitute;

namespace Notification.Api.Infrastructure.Repository.Tests.Mongo.Context.DbContextTests;

[TestClass]
public class WhenCallingGetCollectionWithName
{
    private readonly Fixture _fixture = new();
    private readonly IMongoCollection<Email> _act;
    private readonly string _databaseName;
    private readonly string _collectionName;

    public WhenCallingGetCollectionWithName()
    {
        // Arrange
        _databaseName = _fixture.Create<string>();
        _collectionName = nameof(Email);
        var mongoSettings = new MongoSettings
        {
            ConnectionString = "mongodb://test",
            DatabaseName = _databaseName
        };

        var optionsMongoSettings = Substitute.For<IOptions<MongoSettings>>();
        var mongoClient = Substitute.For<IMongoClient>();

        optionsMongoSettings.Value.Returns(mongoSettings);
        mongoClient.GetDatabase(
            Arg.Is(mongoSettings.DatabaseName),
            Arg.Any<MongoDatabaseSettings>()
            ).Returns(Substitute.For<IMongoDatabase>());

        var dbContext = new DbContext(optionsMongoSettings, Substitute.For<IEnumerable<ICollectionMapper>>());

        // Act
        _act = dbContext.GetCollection<Email>();
    }

    [TestMethod]
    public void ShouldReturnCollectionNotNull()
        => _act.Should().NotBeNull();

    [TestMethod]
    public void ShouldReturnCorrectDatabaseName()
    => _act.Database.DatabaseNamespace.DatabaseName.Should().Be(_databaseName);

    [TestMethod]
    public void ShouldReturnCorrectCollectionName()
        => _act.CollectionNamespace.CollectionName.Should().Be(_collectionName);
}
