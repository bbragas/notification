using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using Notification.Api.Domain;
using Notification.Api.Infrastructure.Repository.Mongo.Base;
using Notification.Api.Infrastructure.Repository.Mongo.Context;
using NSubstitute;

namespace Notification.Api.Infrastructure.Repository.Tests.Mongo.Repository.SmsRepositoryTests;

public class WhenCreatingSmsAsync
{
    private readonly Fixture _fixture = new();
    private readonly IMongoCollection<Sms> _mongoCollection;
    private readonly Sms _sms;

    public WhenCreatingSmsAsync()
    {
        //Arrange
        _sms = _fixture.Create<Sms>();

        _mongoCollection = Substitute.For<IMongoCollection<Sms>>();
        var dbContext = Substitute.For<IDbContext>();

        dbContext.GetCollection<Sms>().Returns(_mongoCollection);

        var smsRepository = new WriteRepository(dbContext);

        //Act
        smsRepository.CreateAsync(_sms, default).Wait();
    }

    [TestMethod]
    public async Task ShouldReceiveOneCallInsertOneAsyncCorrectly()
    => await _mongoCollection.Received().InsertOneAsync(Arg.Is(_sms), Arg.Any<InsertOneOptions>(), Arg.Any<CancellationToken>());
}

