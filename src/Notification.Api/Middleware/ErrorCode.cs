using Notification.Api.Common.Extensions;
using System.Net;
using System.Text.Json.Serialization;

namespace Notification.Api.Middleware
{
    public class ErrorCode
    {
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; }
        public string Code { get; }
        public string Detail { get; }

        public static readonly ErrorCode UserAgentHeaderIsMandatory =
                new ErrorCode("HDR.400.1102", "User-Agent header is mandatory.", HttpStatusCode.BadRequest);
        public static readonly ErrorCode TraceIdHeaderIsMandatory =
                new ErrorCode("HDR.400.1103", "TraceId header is mandatory.", HttpStatusCode.BadRequest);

        private ErrorCode(string code, string detail, HttpStatusCode statusCode)
        {
            Code = code ?? throw new ArgumentNullException(nameof(code), message: null);
            Detail = detail ?? throw new ArgumentNullException(nameof(detail), message: null);
            StatusCode = statusCode.IsDefined()
                ? statusCode
                : throw new ArgumentOutOfRangeException(nameof(statusCode), statusCode, null);
        }
    }
}
