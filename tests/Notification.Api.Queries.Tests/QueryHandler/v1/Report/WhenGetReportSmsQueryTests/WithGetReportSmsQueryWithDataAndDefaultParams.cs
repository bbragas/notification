using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notification.Api.Infrastructure.Repository.Mongo.Base;
using Notification.Api.Queries.QueryHandlers.v1.Report.GetReportSms;
using NSubstitute;

namespace Notification.Api.Queries.Tests.QueryHandler.v1.Report.WhenGetReportSmsQueryTests
{
    [TestClass]
    public class WithGetReportSmsQueryWithDataAndDefaultParams
    {
        private readonly IReadRepository _repository;
        private readonly Fixture _fixture = new();
        private readonly IEnumerable<Domain.Sms> _response;

        public WithGetReportSmsQueryWithDataAndDefaultParams()
        {
            _repository = Substitute.For<IReadRepository>();
            _response = _fixture.Build<Domain.Sms>().CreateMany().ToList();
            string[] projectIds = _response.Select(s => s.ProjectId.ToString()).ToArray();
            _repository.Get<Domain.Sms>()
                .Returns(_response.AsQueryable());

            new GetReportSmsQueryHandler(_repository).Handle(new GetReportSmsQueryRequest(DateTime.MinValue, DateTime.MaxValue, projectIds), CancellationToken.None).Wait();
        }

        [TestMethod]
        public void Should_Received_Get_With_Default_Params()
           => _repository.Received()
              .Get<Domain.Sms>();

    }
}
