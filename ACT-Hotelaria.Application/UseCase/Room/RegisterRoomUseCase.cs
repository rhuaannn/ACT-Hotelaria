 using ACT_Hotelaria.Domain.Abstract;
 using ACT_Hotelaria.Domain.Repository.RoomRepository;
 using MediatR;
 using Microsoft.Extensions.Logging;

 namespace ACT_Hotelaria.Application.UseCase.Room;

public class RegisterRoomUseCase : IRequestHandler<RegisterRoomUseCaseRequest, RegisterRoomUseCaseResponse>
{
    private readonly IWriteOnlyRoomRepository _roomRepository;
    private readonly ILogger<RegisterRoomUseCase> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterRoomUseCase(IWriteOnlyRoomRepository roomRepository, ILogger<RegisterRoomUseCase> logger, IUnitOfWork unitOfWork)
    {
        _roomRepository = roomRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<RegisterRoomUseCaseResponse> Handle(RegisterRoomUseCaseRequest request, CancellationToken cancellationToken)
    {
        var room = Domain.Entities.Room.Create(
            request.TypeRoom,
            request.Quantity
        );
        await _roomRepository.Add(room);
        _logger.LogInformation("Room {TypeRoom} cadastrado com sucesso.", room.Type);
        await _unitOfWork.CommitAsync(cancellationToken);
        return new RegisterRoomUseCaseResponse
        {
            Id = room.Id,
            TypeRoom = room.Type,
            Quantity = room.QtyRoom
        };
    }

     
}