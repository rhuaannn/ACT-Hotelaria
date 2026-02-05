using MediatR;

namespace ACT_Hotelaria.Application.Abstract.Query
{
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
    }
}