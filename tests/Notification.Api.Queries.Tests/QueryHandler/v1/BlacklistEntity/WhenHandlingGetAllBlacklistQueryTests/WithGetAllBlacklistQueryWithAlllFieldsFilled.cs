using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notification.Api.Infrastructure.Repository.Mongo.Base;
using Notification.Api.Messages.Abstractions.Pagination;
using Notification.Api.Queries.QueryHandlers.v1.BlacklistEntity.GetAllBlacklistQuery;
using NSubstitute;

namespace Notification.Api.Queries.Tests.QueryHandler.v1.BlacklistEntity.WhenHandlingGetAllBlacklistQueryTests
{
    [TestClass]
    public class WithGetAllBlacklistQueryWithAlllFieldsFilled
    {
        private readonly IReadRepository _repository;
        private readonly Fixture _fixture = new();

        public WithGetAllBlacklistQueryWithAlllFieldsFilled()
        {
            _repository = Substitute.For<IReadRepository>();

            new GetAllBlacklistQueryHandler(_repository).Handle(_fixture.Create<GetAllBlacklistQueryRequest>(), CancellationToken.None).Wait();
        }

        [TestMethod]
        public async Task Should_Received_GetAllDynamicallyAsync_With_Fields_Filled()
        {
            await _repository.Received()
                .GetAllDynamicallyAsync(
                    Arg.Any<Paging>(),
                    Arg.Any<Func<IQueryable<Domain.BlacklistEntity>, IQueryable<Domain.BlacklistEntity>>>(),
                    Arg.Any<Func<IQueryable<Domain.BlacklistEntity>, IOrderedQueryable<Domain.BlacklistEntity>>>(),
                    Arg.Any<CancellationToken>());
        }
    }
}
