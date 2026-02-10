namespace ACT_Hotelaria.Application.UseCase.Consumption;

public class RegisterConsumptionUseCaseResponse
{
    public Guid ReservationId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}