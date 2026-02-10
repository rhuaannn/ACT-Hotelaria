using MediatR;

namespace ACT_Hotelaria.Application.Abstract.Command
{
    public interface ICommand : IRequest { }

    public interface ICommand<TResponse> : IRequest<TResponse> { }
}
 