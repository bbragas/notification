using Notification.Api.Errors.ErrorDetailCreators;

namespace Notification.Api.Errors;
public class ExceptionDetailFactory : IExceptionDetailFactory
{
    private readonly IReadOnlyDictionary<Type, IExceptionDetailCreator> _errorMap;

    public ExceptionDetailFactory(IEnumerable<IExceptionDetailCreator> exceptionDetailCreators)
    {
        _errorMap = exceptionDetailCreators.ToDictionary(p => p.Type);
    }

    public ErrorDetails GetErrorsDetails(Exception exception)
    {
        var type = typeof(Exception);
        if (_errorMap.ContainsKey(exception.GetType()))
        {
            type = exception.GetType();
        }

        return _errorMap[type].GetErrorDetails(exception);
    }
}