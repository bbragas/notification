using MediatR;
using Notification.Api.Infrastructure.Repository.Mongo.Base;
using Notification.Api.Messages.Abstractions.Pagination;
using Notification.Api.Messages.BlacklistEntity.Adapter;
using Notification.Api.Messages.BlacklistEntity.DTOs;
using System.Linq.Dynamic.Core;

namespace Notification.Api.Queries.QueryHandlers.v1.BlacklistEntity.GetAllBlacklistQuery
{
    public class GetAllBlacklistQueryHandler : IRequestHandler<GetAllBlacklistQueryRequest, PaginatedDTO<BlacklistItemResponseDTO>>
    {
        private readonly IReadRepository _repository;

        public GetAllBlacklistQueryHandler(IReadRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedDTO<BlacklistItemResponseDTO>> Handle(GetAllBlacklistQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllDynamicallyAsync<Domain.BlacklistEntity>(
                        new Paging
                        {
                            PerPage = request.PerPage ?? 0,
                            Page = request.Page ?? 1
                        },
                        projections => BuildWhere(projections, request),
                        projections => projections.OrderBy(o => o.Contact),
                        cancellationToken);

            return result.ToDTO();
        }

        private IQueryable<Domain.BlacklistEntity> BuildWhere(IQueryable<Domain.BlacklistEntity> projections, GetAllBlacklistQueryRequest request)
        {
            if (request.ProjectId.HasValue)
                projections = projections.Where(w => w.ProjectId.Equals(request.ProjectId));
            if (request.ProjectEntityId.HasValue)
                projections = projections.Where(w => (w.ProjectEntityId == null || w.ProjectEntityId.Equals(request.ProjectEntityId)));
            if (!string.IsNullOrEmpty(request.Contact))
                projections = projections.Where(w => (w.Contact.Equals(request.Contact)));

            return projections;
        }
    }

}
