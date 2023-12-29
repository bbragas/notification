using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notification.Api.Infrastructure.Repository.Mongo.Base;
using Notification.Api.Queries.QueryHandlers.v1.BlacklistEntity.GetBlacklistItemQuery;
using NSubstitute;
using System.Linq.Expressions;

namespace Notification.Api.Queries.Tests.QueryHandler.v1.BlacklistEntity.WhenGetBlacklistItemQueryTests
{
    [TestClass]
    public class WithGetBlacklistItemQueryWithAllFieldsDefault
    {
        private readonly IReadRepository _repository;

        public WithGetBlacklistItemQueryWithAllFieldsDefault()
        {
            _repository = Substitute.For<IReadRepository>();

            new GetBlacklistItemQueryHandler(_repository).Handle(new GetBlacklistItemQueryRequest(default, default, default, default), CancellationToken.None).Wait();
        }

        [TestMethod]
        public async Task Should_Received_GetSingleOrDefaultByFilterAsync_With_Default_Fields()
        {
            await _repository.Received()
                .GetSingleOrDefaultByFilterAsync(
                    Arg.Any<Expression<Func<Domain.BlacklistEntity, bool>>>(),
                    Arg.Any<CancellationToken>());
        }
    }
}
