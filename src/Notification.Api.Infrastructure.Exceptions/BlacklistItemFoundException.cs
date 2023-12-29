namespace Notification.Api.Infrastructure.Exceptions;

[Serializable]
public class BlacklistItemFoundException : Exception
{
    public BlacklistItemFoundException(Type type, string contact) : base($"{type.Name} found for contact {contact}")
    {
    }
}
