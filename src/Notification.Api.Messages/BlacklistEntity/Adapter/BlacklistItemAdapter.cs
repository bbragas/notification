using Notification.Api.Messages.Abstractions.Pagination;
using Notification.Api.Messages.BlacklistEntity.DTOs;

namespace Notification.Api.Messages.BlacklistEntity.Adapter
{
    public static class BlacklistItemAdapter
    {
        public static PaginatedDTO<BlacklistItemResponseDTO> ToDTO(this IPagedResult<Domain.BlacklistEntity> blacklistPaged)
            => blacklistPaged is null ? null : new(blacklistPaged.Items.ToDTO(), blacklistPaged.Total);

        public static IReadOnlyCollection<BlacklistItemResponseDTO> ToDTO(this IEnumerable<Domain.BlacklistEntity> domain)
            => domain?.Select(d => d.ToDTO()).ToList();

        public static BlacklistItemResponseDTO ToDTO(this Domain.BlacklistEntity domain) =>
           domain == null ? null : new()
           {
               Id = domain.Id,
               ProjectId = domain.ProjectId,
               ProjectEntityId = domain.ProjectEntityId,
               Contact = domain.Contact,
               Description = domain.Description
           };
    }
}
