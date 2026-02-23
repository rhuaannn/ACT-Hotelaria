using System.Net;
using ACT_Hotelaria.Domain.Exception;

namespace ACT_Hotelaria.Domain.Exception;

public class DomainException : BaseException 
{
    public DomainException(string message) : base(HttpStatusCode.BadRequest,message)
    {
    }
    public DomainException(string message, System.Exception innerException) : base(HttpStatusCode.BadRequest, message, innerException)
    {
    }
}