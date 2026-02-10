using ACT_Hotelaria.Domain.Enum;
using MediatR;

namespace ACT_Hotelaria.Application.UseCase.Room;

public class RegisterRoomUseCaseRequest : IRequest<RegisterRoomUseCaseResponse>
{
    public TypeRoomReservationEnum TypeRoom { get; set; }
    public int Quantity { get; set; }
}