using System.Text.Json;
using ACT_Hotelaria.Domain.Repository.ClientRepository;
using ACT_Hotelaria.Domain.Repository.DependentRepository;
using ACT_Hotelaria.Domain.ValueObject;
using ACT_Hotelaria.Redis.Repository;

namespace ACT_Hotelaria.Application.UseCase.Client;

public class RegisterClientUseCase
{
    private readonly IWriteOnlyClientRepository _clientRepository;
    private readonly IReadOnlyClientRepository _readOnlyClientRepository;

    
    public RegisterClientUseCase(IWriteOnlyClientRepository clientRepository,
        IReadOnlyClientRepository readOnlyClientRepository
        )
    {
        _clientRepository = clientRepository;
        _readOnlyClientRepository = readOnlyClientRepository;
    }

    public async Task<RegisterClientUseCaseResponse> Handle(RegisterClientUseCaseRequest request)
    {
        var email = Email.Create(request.Email);
        var cpf = Cpf.Create(request.CPF);
        var telefone = Telefone.Create(request.Phone);
        
        var client = Domain.Entities.Client.Create(request.Name, cpf, email, telefone);
        var exists = await _readOnlyClientRepository.ExistsCpf(cpf.Value);
        
        if (exists)
        {
            throw new ArgumentException("Cpf já cadastrado");
        }
        
        if (request.Dependents != null)
        {
            foreach (var dep in request.Dependents)
            {
                if (string.IsNullOrWhiteSpace(dep.CPF) || string.IsNullOrWhiteSpace(dep.Name))
                {
                    throw new ArgumentException("Os dados dos dependente precisam ser preenchido corretamente");
                }
                var depCPF = Cpf.Create(dep.CPF);
                client.AddDependent(dep.Name, depCPF);
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
