using ACT_Hotelaria.Domain.Enum;
using MediatR;

namespace ACT_Hotelaria.Application.UseCase.Reservation;

public class RegisterReservationUseCaseRequest : IRequest<RegisterReservationUseCaseResponse>
{
    public Guid ClientId { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public decimal AgreedDailyRate { get; set; }
    public Guid RoomId { get; set; }
     
    
}