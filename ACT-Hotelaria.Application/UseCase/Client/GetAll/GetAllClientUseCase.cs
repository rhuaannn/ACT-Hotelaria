using System.Collections;
using ACT_Hotelaria.Domain.Entities;
using ACT_Hotelaria.Domain.Repository.ClientRepository;
using ACT_Hotelaria.Domain.Repository.cs.Reservation;
using ACT_Hotelaria.Domain.Repository.DependentRepository;

namespace ACT_Hotelaria.Application.UseCase.Client.GetAll;

public class GetAllClientUseCase
{
    private readonly IReadOnlyClientRepository _readOnlyClientRepository;
    private readonly IReadOnlyReservationRepository _readOnlyReservationRepository;
    private readonly IReadOnlyDependentRepository   _readOnlyDependentRepository;

    public GetAllClientUseCase(
        IReadOnlyClientRepository readOnlyClientRepository, 
        IReadOnlyReservationRepository readOnlyReservationRepository, 
        IReadOnlyDependentRepository readOnlyDependentRepository)
    {
        _readOnlyClientRepository = readOnlyClientRepository;
        _readOnlyReservationRepository = readOnlyReservationRepository;
        _readOnlyDependentRepository = readOnlyDependentRepository;
    }

    public async Task<IEnumerable<GetAllClientResponse>> Handle()
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