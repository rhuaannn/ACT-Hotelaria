using ACT_Hotelaria.Domain.Repository.ClientRepository;
using ACT_Hotelaria.Domain.Repository.Reservation;

namespace ACT_Hotelaria.Application.UseCase.Reservation.GetAll;

public class GetAllReservationUseCase
{
    private readonly IReadOnlyReservationRepository _readOnlyReservationRepository;
    private readonly IReadOnlyClientRepository _readOnlyClientRepository;

    public GetAllReservationUseCase(IReadOnlyClientRepository readOnlyClientRepository, IReadOnlyReservationRepository readOnlyReservationRepository)
    {
        _readOnlyClientRepository = readOnlyClientRepository;
        _readOnlyReservationRepository = readOnlyReservationRepository;
    }

    public async Task<IEnumerable<GetAllReservationUseCaseResponse>> Handle()
    {
        var client = await _readOnlyClientRepository.GetAll();

        var reservation = await _readOnlyReservationRepository.GetAll();

        var response = reservation.Select(reserva => new GetAllReservationUseCaseResponse
        {
            Id = reserva.Id,
            CheckIn = reserva.Checkin,
            CheckOut = reserva.Checkout,
            ClientId = reserva.ClientId,
            ClientName = reserva.Client?.Name ?? "Cliente não identificado"
        });
        return response;
    }
}