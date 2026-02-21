using System.Text.Json;
using ACT_Hotelaria.Domain.Abstract;
using ACT_Hotelaria.Domain.Exception;
using ACT_Hotelaria.Domain.Repository.ClientRepository;
using ACT_Hotelaria.Domain.Repository.DependentRepository;
using ACT_Hotelaria.Domain.ValueObject;
using ACT_Hotelaria.Message;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ACT_Hotelaria.Application.UseCase.Client;

public class RegisterClientUseCase : IRequestHandler<RegisterClientUseCaseRequest, RegisterClientUseCaseResponse>
{
    private readonly IWriteOnlyClientRepository _clientRepository;
    private readonly IReadOnlyClientRepository _readOnlyClientRepository;
    private readonly IReadOnlyDependentRepository _readOnlyDependetRepository;
    private readonly ILogger<RegisterClientUseCase> _logger;
    private readonly IUnitOfWork _unitOfWork;
    
    public RegisterClientUseCase(IWriteOnlyClientRepository clientRepository,
        IReadOnlyClientRepository readOnlyClientRepository,
        IReadOnlyDependentRepository readOnlyDependetRepository,
        ILogger<RegisterClientUseCase> logger,
        IUnitOfWork unitOfWork
        )
    {
        _clientRepository = clientRepository;
        _readOnlyClientRepository = readOnlyClientRepository;
        _readOnlyDependetRepository = readOnlyDependetRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<RegisterClientUseCaseResponse> Handle(RegisterClientUseCaseRequest request, CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);
        var cpf = Cpf.Create(request.CPF);
        var telefone = Telefone.Create(request.Phone);
        
        var client = Domain.Entities.Client.Create(request.Name, cpf, email, telefone);
        var exists = await _readOnlyClientRepository.ExistsCpf(cpf.Value);
        
      
        if (exists)
        {
            _logger.LogError(JsonSerializer.Serialize(client));
            throw new DomainException(ResourceMessages.CPFJaCadastrado);
        }
        
        if (request.Dependents != null)
        {
            foreach (var dep in request.Dependents)
            {
                if (string.IsNullOrWhiteSpace(dep.CPF) || string.IsNullOrWhiteSpace(dep.Name))
                {
                    _logger.LogError(JsonSerializer.Serialize(dep));
                    throw new DomainException(ResourceMessages.PreenchimentoDependenteObrigatorio);
                }

                var depCPF = Cpf.Create(dep.CPF);
                var existsCPFDependent = await _readOnlyDependetRepository.ExistsCpf(depCPF.Value);
                if (existsCPFDependent)
                {
                    throw new DomainException(ResourceMessages.CPFJaCadastrado);
                }
                client.AddDependent(dep.Name, depCPF);

            }
        }
        await _clientRepository.Add(client);
        await _unitOfWork.CommitAsync(cancellationToken);
        _logger.LogInformation($"Cliente {client.Name} cadastrado com sucesso!");
        return new RegisterClientUseCaseResponse
        {
            Id = client.Id,
            Name = client.Name,

        };

    }
}
