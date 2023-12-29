using MediatR;
using Notification.Api.Messages.BlacklistEntity.DTOs;

namespace Notification.Api.Queries.QueryHandlers.v1.BlacklistEntity.GetAllBlacklistQuery;

public record GetAllBlacklistQueryRequest(Guid? ProjectId, Guid? ProjectEntityId, string Contact, int? PerPage, int? Page) : IRequest<PaginatedDTO<BlacklistItemResponseDTO>>;
