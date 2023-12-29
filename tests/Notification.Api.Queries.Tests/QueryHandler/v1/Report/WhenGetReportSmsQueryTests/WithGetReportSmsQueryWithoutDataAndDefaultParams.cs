using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notification.Api.Infrastructure.Repository.Mongo.Base;
using Notification.Api.Queries.QueryHandlers.v1.Report.GetReportSms;
using NSubstitute;

namespace Notification.Api.Queries.Tests.QueryHandler.v1.Report.WhenGetReportSmsQueryTests
{
    [TestClass]
    public class WithGetReportSmsQueryWithoutDataAndDefaultParams
    {
        private readonly IReadRepository _repository;

        public WithGetReportSmsQueryWithoutDataAndDefaultParams()
        {
            string[] projectIds = { };
            _repository = Substitute.For<IReadRepository>();

            new GetReportSmsQueryHandler(_repository).Handle(new GetReportSmsQueryRequest(default, default, projectIds), CancellationToken.None).Wait();
        }

        [TestMethod]
        public void Should_Received_Get_With_Default_Params()
            => _repository.Received()
               .Get<Domain.Sms>();

    }
}
