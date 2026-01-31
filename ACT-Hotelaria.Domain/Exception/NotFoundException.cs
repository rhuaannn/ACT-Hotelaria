using System.Net;

namespace ACT_Hotelaria.Domain.Exception;

public class NotFoundException : BaseException
{
    public NotFoundException(string message) : base(HttpStatusCode.NotFound, message)
    {
    }

    public NotFoundException(string message, System.Exception innerException) : base(HttpStatusCode.NotFound,
        message, innerException)
    {
    }
}