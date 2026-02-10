using MediatR;

namespace ACT_Hotelaria.Application.UseCase.Consumption;

public class RegisterConsumptionUseCaseRequest : IRequest<RegisterConsumptionUseCaseResponse>
{
    public Guid ProductId { get; set; }
    public Guid ReservationId { get; set; }
    public int Quantity { get; set; }
}