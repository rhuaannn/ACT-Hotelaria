namespace ACT_Hotelaria.Application.UseCase.Reservation;

public class RegisterReservationUseCaseResponse
{
    public Guid Id { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public decimal TotalPrice { get; set; }
}