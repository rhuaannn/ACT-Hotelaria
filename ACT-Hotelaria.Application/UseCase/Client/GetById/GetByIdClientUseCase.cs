using System.Text.Json;
using ACT_Hotelaria.Application.Abstract.Query;
using ACT_Hotelaria.Domain.Exception;
using ACT_Hotelaria.Domain.Interface;
using ACT_Hotelaria.Domain.Notification;
using ACT_Hotelaria.Domain.Repository.ClientRepository;
using ACT_Hotelaria.Domain.Repository.DependentRepository;
using ACT_Hotelaria.Message;
using ACT_Hotelaria.Redis.Repository;

namespace ACT_Hotelaria.Application.UseCase.Client.GetById;

public class GetByIdClientUseCase : IQueryHandler<GetByIdQueryClientUseCase, GetByIdClientUseCaseResponse>
{
    private readonly IReadOnlyClientRepository _readOnlyClientRepository;
    private readonly ICaching _caching;
    private readonly INotification _notification;
    
    public GetByIdClientUseCase(IReadOnlyClientRepository readOnlyClientRepository, ICaching caching, INotification notification)
    {
        _readOnlyClientRepository = readOnlyClientRepository;
        _caching = caching;
        _notification = notification;
    }

    public async Task<GetByIdClientUseCaseResponse> Handle(GetByIdQueryClientUseCase request, CancellationToken cancellationToken)
    {
        var cacheKey = $"client:{request.Id}";
        var cachedJson = await _caching.GetAsync(cacheKey);
        if (!string.IsNullOrEmpty(cachedJson))
        {
            var cachedResponse = JsonSerializer.Deserialize<GetByIdClientUseCaseResponse>(cachedJson);
            if (cachedResponse != null)
            {
                return cachedResponse;
            }
        }

        var client = await _readOnlyClientRepository.GetById(request.Id);

        if (client == null)
        {
            _notification.Handle(new Notification(ResourceMessages.ClienteNaoEncontrado));
        }

        var response = new GetByIdClientUseCaseResponse
        {
            Id = client.Id,
            Name = client.Name,
            Email = client.Email.Value,
            CPF = client.CPF.Value,  

            Dependents = client.Dependents.Select(dep => new GetByIdDependentResponse
            {
                Id = dep.Id,
                Name = dep.Name,
                CPF = dep.CPF.Value
            }).ToList()
        };

        var jsonToCache = JsonSerializer.Serialize(response);
        await _caching.SetAsync(cacheKey, jsonToCache);

        return response;
    }
}
