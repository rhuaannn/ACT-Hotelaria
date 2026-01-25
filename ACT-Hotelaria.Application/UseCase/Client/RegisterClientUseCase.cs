using ACT_Hotelaria.Domain.Repository.ClientRepository;
using ACT_Hotelaria.Domain.Repository.DependentRepository;
using ACT_Hotelaria.Domain.ValueObject;

namespace ACT_Hotelaria.Application.UseCase.Client;

public class RegisterClientUseCase
{
    private readonly IWriteOnlyClientRepository _clientRepository;
    
    public RegisterClientUseCase(IWriteOnlyClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<RegisterClientUseCaseResponse> Handle(RegisterClientUseCaseRequest request)
    {
        var email = Email.Create(request.Email);
        var cpf = Cpf.Create(request.CPF);
        var telefone = Telefone.Create(request.Phone);
        
        var client = Domain.Entities.Client.Create(request.Name, cpf, email, telefone);
        
        if (request.Dependents != null && request.Dependents.Any())
        {
            foreach (var dep in request.Dependents)
            {
                client.AddDependent(dep.Name, new Cpf(dep.CPF));
            }
        }
        await _clientRepository.Add(client);

        return new RegisterClientUseCaseResponse
        {
            Id = client.Id,
            Name = client.Name,

        };

    }
}
