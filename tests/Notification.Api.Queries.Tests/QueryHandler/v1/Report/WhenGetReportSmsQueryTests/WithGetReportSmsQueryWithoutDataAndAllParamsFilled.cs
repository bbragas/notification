using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notification.Api.Infrastructure.Repository.Mongo.Base;
using Notification.Api.Queries.QueryHandlers.v1.Report.GetReportSms;
using NSubstitute;

namespace Notification.Api.Queries.Tests.QueryHandler.v1.Report.WhenGetReportSmsQueryTests
{
    [TestClass]
    public class WithGetReportSmsQueryWithoutDataAndAllParamsFilled
    {
        private readonly IReadRepository _repository;

        public WithGetReportSmsQueryWithoutDataAndAllParamsFilled()
        {
            _repository = Substitute.For<IReadRepository>();
            string[] projectIds = { };

            new GetReportSmsQueryHandler(_repository).Handle(new GetReportSmsQueryRequest(DateTime.MinValue, DateTime.MaxValue, projectIds), CancellationToken.None).Wait();
        }

        [TestMethod]
        public void Should_Received_Get_With_Params_Filled()
            => _repository.Received()
               .Get<Domain.Sms>();

    }
}
