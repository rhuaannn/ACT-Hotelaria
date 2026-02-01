namespace ACT_Hotelaria.Application.UseCase.Consumption;

public class RegisterConsumptionUseCaseRequest
{
    public Guid ProductId { get; set; }
    public Guid ReservationId { get; set; }
    public int Quantity { get; set; }
}