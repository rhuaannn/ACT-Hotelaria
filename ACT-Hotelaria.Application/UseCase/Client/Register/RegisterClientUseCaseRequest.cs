using MediatR;

namespace ACT_Hotelaria.Application.UseCase.Client;

public record RegisterClientUseCaseRequest : IRequest<RegisterClientUseCaseResponse>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string CPF { get; set; }
    public string Phone { get; set; }
    public List<CreateDependentInput> Dependents { get; set; } = new();
}
public record CreateDependentInput(string Name, string CPF);