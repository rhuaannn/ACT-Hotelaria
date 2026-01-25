using ACT_Hotelaria.Domain.Entities;

namespace ACT_Hotelaria.Application.UseCase.Client.GetAll;

public class GetAllClientResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string CPF { get; set; }
    public string Phone { get; set; }
    public List<GetAllDependentResponse> Dependents { get; set; } = new();
}