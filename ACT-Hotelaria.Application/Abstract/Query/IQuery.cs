using MediatR;

namespace ACT_Hotelaria.Application.Abstract.Query
{
    public interface IQuery<TResponse> : IRequest<TResponse> { }

}