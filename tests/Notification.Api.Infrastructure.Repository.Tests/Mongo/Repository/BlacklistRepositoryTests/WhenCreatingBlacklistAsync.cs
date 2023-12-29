using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using Notification.Api.Domain;
using Notification.Api.Infrastructure.Repository.Mongo.Base;
using Notification.Api.Infrastructure.Repository.Mongo.Context;
using NSubstitute;

namespace Notification.Api.Infrastructure.Repository.Tests.Mongo.Repository.BlacklistRepositoryTests
{
    [TestClass]
    public class WhenCreatingBlacklistAsync
    {
        private readonly Fixture _fixture = new();
        private readonly IMongoCollection<BlacklistEntity> _mongoCollection;
        private readonly BlacklistEntity _blacklist;

        public WhenCreatingBlacklistAsync()
        {
            //Arrange
            _blacklist = _fixture.Create<BlacklistEntity>();

            _mongoCollection = Substitute.For<IMongoCollection<BlacklistEntity>>();
            var dbContext = Substitute.For<IDbContext>();

            dbContext.GetCollection<BlacklistEntity>().Returns(_mongoCollection);

            var repository = new WriteRepository(dbContext);

            //Act
            repository.CreateAsync(_blacklist, default).Wait();
        }

        [TestMethod]
        public async Task ShouldReceiveOneCallInsertOneAsyncCorrectly()
            => await _mongoCollection.Received().InsertOneAsync(Arg.Is(_blacklist), Arg.Any<InsertOneOptions>(), Arg.Any<CancellationToken>());
    }
}
