namespace ACT_Hotelaria.Application.UseCase.Consumption.GetById;

public record GetByIdConsumptionUseCaseResponse
{
    public Guid Id { get; set; }
    public decimal Value { get; set; }
    public int Quantity { get; set; }
}