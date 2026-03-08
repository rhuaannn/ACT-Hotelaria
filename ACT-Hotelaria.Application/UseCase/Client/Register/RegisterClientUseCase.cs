using ACT_Hotelaria.Application.UseCase.Client;
using ACT_Hotelaria.Domain.Abstract;
using ACT_Hotelaria.Domain.DomainNotification;
using ACT_Hotelaria.Domain.Entities;
using ACT_Hotelaria.Domain.Repository.ClientRepository;
using ACT_Hotelaria.Domain.Repository.DependentRepository;
using ACT_Hotelaria.Domain.ValueObject;
using ACT_Hotelaria.Message;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

public class RegisterClientUseCase : IRequestHandler<RegisterClientUseCaseRequest, RegisterClientUseCaseResponse>
{
    private readonly IWriteOnlyClientRepository _clientRepository;
    private readonly IReadOnlyClientRepository _readOnlyClientRepository;
    private readonly IReadOnlyDependentRepository _readOnlyDependetRepository;
    private readonly ILogger<RegisterClientUseCase> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<RegisterClientUseCaseRequest> _validator;
    private readonly NotificationContext _notificationContext;

    public RegisterClientUseCase(
        IWriteOnlyClientRepository clientRepository,
        IReadOnlyClientRepository readOnlyClientRepository,
        IReadOnlyDependentRepository readOnlyDependetRepository,
        ILogger<RegisterClientUseCase> logger,
        IUnitOfWork unitOfWork,
        IValidator<RegisterClientUseCaseRequest> validator,
        NotificationContext notificationContext)
    {
        _clientRepository = clientRepository;
        _readOnlyClientRepository = readOnlyClientRepository;
        _readOnlyDependetRepository = readOnlyDependetRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _notificationContext = notificationContext;
    }

    public async Task<RegisterClientUseCaseResponse?> Handle(RegisterClientUseCaseRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            _notificationContext.AddNotifications(validationResult);
        }

        var email = Email.Create(request.Email);
        var cpf = Cpf.Create(request.CPF);
        var telefone = Telefone.Create(request.Phone);

        if (await _readOnlyClientRepository.ExistsCpf(cpf.Value))
        {
            _notificationContext.AddNotification("CPF", ResourceMessages.CPFJaCadastrado);
        }

        if (_notificationContext.HasNotifications)
        {
            return null;
        }

        var client = Client.Create(request.Name, cpf, email, telefone);

        if (request.Dependents != null)
        {
            foreach (var dep in request.Dependents)
            {
                if (string.IsNullOrWhiteSpace(dep.CPF) || string.IsNullOrWhiteSpace(dep.Name))
                {
                    _notificationContext.AddNotification("Dependente", ResourceMessages.PreenchimentoDependenteObrigatorio);
                    return null;
                }

                var depCPF = Cpf.Create(dep.CPF);
                if (await _readOnlyDependetRepository.ExistsCpf(depCPF.Value))
                {
                    _notificationContext.AddNotification("DependenteCPF", ResourceMessages.CPFJaCadastrado);
                    return null;
                }

                client.AddDependent(dep.Name, depCPF);
            }
        }

        await _clientRepository.Add(client);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        _logger.LogInformation("Cliente {ClientName} cadastrado com sucesso!", client.Name);

        return new RegisterClientUseCaseResponse { Id = client.Id, Name = client.Name };
    }
}