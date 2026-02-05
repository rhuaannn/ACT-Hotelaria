using MediatR;

namespace ACT_Hotelaria.Application.Abstract.Command

{
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand> where TCommand : ICommand { }

    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse> where TCommand : ICommand<TResponse> { }

}