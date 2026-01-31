using System.Text.Json;
using ACT_Hotelaria.Domain.Exception;
using ACT_Hotelaria.Domain.Repository.ClientRepository;
using ACT_Hotelaria.Domain.Repository.DependentRepository;
using ACT_Hotelaria.Message;
using ACT_Hotelaria.Redis.Repository;

namespace ACT_Hotelaria.Application.UseCase.Client.GetById;

public class GetByIdClientUseCase
{
    private readonly IReadOnlyClientRepository _readOnlyClientRepository;
    private readonly ICaching _caching;
    
    public GetByIdClientUseCase(IReadOnlyClientRepository readOnlyClientRepository, ICaching caching)
    {
        _readOnlyClientRepository = readOnlyClientRepository;
        _caching = caching;
    }

    public async Task<GetByIdClientUseCaseResponse> Handle(Guid id)
    {
        var cacheKey = $"client:{id}";
        var cachedJson = await _caching.GetAsync(cacheKey);
        if (!string.IsNullOrEmpty(cachedJson))
        {
            var cachedResponse = JsonSerializer.Deserialize<GetByIdClientUseCaseResponse>(cachedJson);
            if (cachedResponse != null)
            {
                return cachedResponse;
            }
        }

        var client = await _readOnlyClientRepository.GetById(id);

        if (client == null)
        {
            throw new NotFoundException(ResourceMessages.ClienteNaoEncontrado);
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
