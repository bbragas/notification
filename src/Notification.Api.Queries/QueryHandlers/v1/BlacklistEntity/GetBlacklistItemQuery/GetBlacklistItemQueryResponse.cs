namespace Notification.Api.Queries.QueryHandlers.v1.BlacklistEntity.GetBlacklistItemQuery
{
    public record GetBlacklistItemQueryResponse(Guid Id, Guid ProjectId, Guid? ProjectEntityId, string Contact, string Description);
}
