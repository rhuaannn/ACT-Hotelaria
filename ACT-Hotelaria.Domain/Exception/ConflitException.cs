using System.Net;

namespace ACT_Hotelaria.Domain.Exception;

public class ConflitException : BaseException
{
    public ConflitException(string message) : base(HttpStatusCode.Conflict, message)
    {
    }

    public ConflitException(string message, System.Exception innerException) : base(HttpStatusCode.Conflict, message, innerException)
    {
    }
}