using Notification.Api.Messages.Abstractions.DTOs;

namespace Notification.Api.Messages.BlacklistEntity.DTOs
{
    public class BlacklistItemResponseDTO: IDataTransferObjectValidator
    {
        public Guid Id { get; init; }
        public Guid ProjectId { get; init; }
        public Guid? ProjectEntityId { get; init; } = null;
        public string Contact { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
    }    
}
