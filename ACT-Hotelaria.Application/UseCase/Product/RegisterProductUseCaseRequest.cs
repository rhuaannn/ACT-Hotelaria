using MediatR;

namespace ACT_Hotelaria.Application.UseCase.Product;

public class RegisterProductUseCaseRequest : IRequest<RegisterProductUseCaseResponse>
{
    public string Name { get; set; }
    public decimal Value { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
}