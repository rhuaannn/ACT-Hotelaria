using ACT_Hotelaria.Domain.Enum;

namespace ACT_Hotelaria.Application.UseCase.Reservation;

public class RegisterReservationUseCaseRequest
{
    public Guid ClientId { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    
    public decimal AgreedDailyRate { get; set; }
    public TypeRoomReservationEnum Type { get; set; }
    
    
}