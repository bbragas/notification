using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using Notification.Api.Infrastructure.Repository.Mongo.Context;
using NSubstitute;
using Notification.Api.Infrastructure.Repository.Mongo.Base;
using Notification.Api.Domain;

namespace Notification.Api.Infrastructure.Repository.Tests.Mongo.Repository.EmailRepositoryTests;

[TestClass]
public class WhenCreatingEmailAsync
{
    private readonly Fixture _fixture = new();
    private readonly IMongoCollection<Email> _mongoCollection;
    private readonly Email _email;

    public WhenCreatingEmailAsync()
    {
        //Arrange
        _email = _fixture.Create<Email>();

        _mongoCollection = Substitute.For<IMongoCollection<Email>>();
        var dbContext = Substitute.For<IDbContext>();

        dbContext.GetCollection<Email>().Returns(_mongoCollection);

        var repository = new WriteRepository(dbContext);

        //Act
        repository.CreateAsync(_email, default).Wait();
    }

    [TestMethod]
    public async Task ShouldReceiveOneCallInsertOneAsyncCorrectly()
        => await _mongoCollection.Received().InsertOneAsync(Arg.Is(_email), Arg.Any<InsertOneOptions>(), Arg.Any<CancellationToken>());

}
