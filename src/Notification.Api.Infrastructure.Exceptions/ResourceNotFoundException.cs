using System.Runtime.Serialization;

namespace Notification.Api.Infrastructure.Exceptions;

[Serializable]
public class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException(Type type) : base($"{type.Name} not found")
    {
    }

    public ResourceNotFoundException(string name) : base($"{name} not found")
    {
    }

    public ResourceNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected ResourceNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}