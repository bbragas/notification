using MediatR;
using Notification.Api.Infrastructure.Repository.Mongo.Base;

namespace Notification.Api.Queries.QueryHandlers.v1.Report.GetReportSms;

public class GetReportSmsQueryHandler : IRequestHandler<GetReportSmsQueryRequest, GetReportSmsQueryResponse>
{
    private readonly IReadRepository _repository;

    public GetReportSmsQueryHandler(IReadRepository repository)
    {
        _repository = repository;
    }

    public Task<GetReportSmsQueryResponse> Handle(GetReportSmsQueryRequest request, CancellationToken cancellationToken)
    {
        var smss = GetSmss(request);

        var dictionary = new Dictionary<string, GetReportSmsItem>();

        foreach (var sms in smss)
        {
            var key = sms.ProjectId + sms.Client + sms.Campaign;

            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, new GetReportSmsItem(sms.ProjectId, sms.Client, sms.Campaign));
            }

            dictionary[key].Total = dictionary[key].Total + 1;
        }

        return Task.FromResult(new GetReportSmsQueryResponse(request.From, request.To, dictionary.Values));
    }

    private Domain.Sms[] GetSmss(GetReportSmsQueryRequest request)
    {
        var query = _repository.Get<Domain.Sms>()
                         .Where(p => request.From < p.CreatedAt)
                         .Where(p => p.CreatedAt < request.To);

        if (request.projectIds.Length > 0)
            query = query.Where(p => request.projectIds.Contains(p.ProjectId));

        return query.ToArray();
    }
}
