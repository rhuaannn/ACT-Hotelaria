using System.Net;

namespace ACT_Hotelaria.Domain.Exception;

public class InternalException : BaseException
{
    public InternalException(string message) : base(HttpStatusCode.InternalServerError, message)
    {
    }
    public InternalException(string message, System.Exception innerException) : base(HttpStatusCode.InternalServerError,
        message, innerException)
    {
    }
}