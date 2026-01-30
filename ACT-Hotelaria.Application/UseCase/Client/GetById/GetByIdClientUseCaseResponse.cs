using ACT_Hotelaria.Application.UseCase.Client.GetAll;
using ACT_Hotelaria.Domain.Entities;

namespace ACT_Hotelaria.Application.UseCase.Client.GetById;

public class GetByIdClientUseCaseResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string CPF { get; set; }
    public List<GetByIdDependentResponse> Dependents { get; set; } = new();

}