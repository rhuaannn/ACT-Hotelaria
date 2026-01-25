namespace ACT_Hotelaria.Application.UseCase.Client;

public record RegisterClientUseCaseResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}