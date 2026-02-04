using ACT_Hotelaria.Domain.Enum;

namespace ACT_Hotelaria.Application.UseCase.Room;

public class RegisterRoomUseCaseResponse
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public TypeRoomReservationEnum TypeRoom { get; set; }
}