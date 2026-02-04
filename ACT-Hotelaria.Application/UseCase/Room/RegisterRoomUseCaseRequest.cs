using ACT_Hotelaria.Domain.Enum;

namespace ACT_Hotelaria.Application.UseCase.Room;

public class RegisterRoomUseCaseRequest
{
    public TypeRoomReservationEnum TypeRoom { get; set; }
    public int Quantity { get; set; }
}