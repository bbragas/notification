using Notification.Api.Messages.Abstractions.DTOs;

namespace Notification.Api.Messages.BlacklistEntity.DTOs
{
    public class GetAllBlacklistDTO : IDataTransferObjectValidator
    {
        public Guid? ProjectId { get; init; } = null;
        public Guid? ProjectEntityId { get; init; } = null;        
        public int? PerPage { get; set; } = null;
        public int? Page { get; set; } = null;
    }
}
