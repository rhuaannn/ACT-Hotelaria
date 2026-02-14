namespace ACT_Hotelaria.Application.UseCase.Consumption.GetAll;

public class GetAllConsumptionUseCaseResponse
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public decimal Value { get; set; }
    public int Quantity { get; set; }
    

}