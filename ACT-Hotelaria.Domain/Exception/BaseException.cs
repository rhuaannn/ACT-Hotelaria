using System.Net;

namespace ACT_Hotelaria.Domain.Exception;

public class BaseException : System.Exception
{
    public HttpStatusCode StatusCode { get; }

    protected BaseException(HttpStatusCode statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }

    protected BaseException(HttpStatusCode statusCode, string message, System.Exception innerException) : base(message, innerException)
    {
        StatusCode = statusCode;
    }
}