 using ACT_Hotelaria.Domain.Repository.RoomRepository;

namespace ACT_Hotelaria.Application.UseCase.Room;

public class RegisterRoomUseCase
{
    private readonly IWriteOnlyRoomRepository _roomRepository;

    public RegisterRoomUseCase(IWriteOnlyRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<RegisterRoomUseCaseResponse> Handle(RegisterRoomUseCaseRequest request)
    {
        var room = Domain.Entities.Room.Create(
            request.TypeRoom,
            request.Quantity
        );
        await _roomRepository.Add(room);
        return new RegisterRoomUseCaseResponse
        {
            Id = room.Id,
            TypeRoom = room.Type,
            Quantity = room.QtyRoom
        };
    }
}