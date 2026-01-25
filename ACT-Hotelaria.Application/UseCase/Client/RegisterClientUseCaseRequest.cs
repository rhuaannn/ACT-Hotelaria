namespace ACT_Hotelaria.Application.UseCase.Client;

public record RegisterClientUseCaseRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string CPF { get; set; }
    public string Phone { get; set; }
    public List<CreateDependentInput>? Dependents { get; set; }
}
public record CreateDependentInput(string Name, string CPF);