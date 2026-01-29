namespace ACT_Hotelaria.Application.UseCase.Reservation.GetAll;

public class GetAllReservationUseCaseResponse
{
    public Guid Id { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public Guid ClientId { get; set; }
    public string ClientName { get; set; }
    
}