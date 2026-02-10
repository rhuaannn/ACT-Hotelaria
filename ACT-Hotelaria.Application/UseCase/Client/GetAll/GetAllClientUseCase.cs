using System.Collections;
using ACT_Hotelaria.Application.Abstract.Query;
using ACT_Hotelaria.Domain.Entities;
using ACT_Hotelaria.Domain.Repository.ClientRepository;
 using ACT_Hotelaria.Domain.Repository.DependentRepository;
 using ACT_Hotelaria.Domain.Repository.Reservation;
 using ACT_Hotelaria.Redis.Repository;

namespace ACT_Hotelaria.Application.UseCase.Client.GetAll;

public class GetAllClientUseCase : IQueryHandler<GetAllQueryClientUseCase, IEnumerable<GetAllClientResponse>>
{
    private readonly IReadOnlyClientRepository _readOnlyClientRepository;
    private readonly IReadOnlyReservationRepository _readOnlyReservationRepository;
    private readonly IReadOnlyDependentRepository   _readOnlyDependentRepository;
    private readonly ICaching _caching;

    public GetAllClientUseCase(
        IReadOnlyClientRepository readOnlyClientRepository, 
        IReadOnlyReservationRepository readOnlyReservationRepository, 
        IReadOnlyDependentRepository readOnlyDependentRepository,
        ICaching caching)
    {
        _readOnlyClientRepository = readOnlyClientRepository;
        _readOnlyReservationRepository = readOnlyReservationRepository;
        _readOnlyDependentRepository = readOnlyDependentRepository;
        _caching = caching;
    }

    public async Task<IEnumerable<GetAllClientResponse>> Handle(GetAllQueryClientUseCase request,
                                                                CancellationToken cancellationToken = default)
   {
        var clients = await _readOnlyClientRepository.GetAll();

        var response = clients.Select(client => new GetAllClientResponse
        {
            Id = client.Id,
            Name = client.Name,
            CPF = client.CPF.Value,       
            Email = client.Email.Value,
            Phone = client.Telefone.Value,

            Dependents = client.Dependents.Select(dep => new GetAllDependentResponse
            {
                Id = dep.Id,
                Name = dep.Name,
                CPF = dep.CPF.Value 
            }).ToList() 
        });
        
        return response;
    }
}