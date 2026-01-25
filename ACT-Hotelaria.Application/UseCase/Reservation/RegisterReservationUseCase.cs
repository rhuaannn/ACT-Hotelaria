using ACT_Hotelaria.Domain.Enum;
using ACT_Hotelaria.Domain.Repository.ClientRepository;
using ACT_Hotelaria.Domain.Repository.cs.Reservation;

namespace ACT_Hotelaria.Application.UseCase.Reservation;

public class RegisterReservationUseCase
{
    private readonly IWriteOnlyReservationRepository _writeOnlyReservationRepository;
    private readonly IReadOnlyReservationRepository _readOnlyReservationRepository;
    private readonly IReadOnlyClientRepository _readOnlyClientRepository;
    private readonly IWriteOnlyClientRepository _writeOnlyClientRepository;

    public RegisterReservationUseCase(
        IReadOnlyClientRepository readOnlyClientRepository, 
        IWriteOnlyClientRepository writeOnlyClientRepository, 
        IWriteOnlyReservationRepository writeOnlyReservationRepository, 
        IReadOnlyReservationRepository readOnlyReservationRepository)
    {
        _readOnlyClientRepository = readOnlyClientRepository;
        _writeOnlyClientRepository = writeOnlyClientRepository;
        _writeOnlyReservationRepository = writeOnlyReservationRepository;
        _readOnlyReservationRepository = readOnlyReservationRepository;
    }

    public async Task<RegisterReservationUseCaseResponse> Handle(RegisterReservationUseCaseRequest request)
    {
        var client = await _readOnlyClientRepository.Exists(request.ClientId);
        if (!client)
        {
            throw new ArgumentException("Cliente não encontrado");
        }

        var dailyRate = GetDailyRateByType(request.Type);
        
        var reservation = Domain.Entities.Reservation.Create(
            request.Type, 
            request.CheckIn, 
            request.CheckOut, 
            dailyRate, 
            request.ClientId);
        
        await _writeOnlyReservationRepository.Add(reservation);
        
        return new RegisterReservationUseCaseResponse{
            Id = request.ClientId,
            CheckIn = request.CheckIn,
            CheckOut = request.CheckOut,
            TotalPrice = reservation.TotalPrice
            };
    }
    private decimal GetDailyRateByType(TypeRoomReservationEnum type)
    {
        return type switch
        {
            TypeRoomReservationEnum.luxury => 100.00m,
            TypeRoomReservationEnum.couple => 250.00m,
            TypeRoomReservationEnum.single => 80.00m,
            _ => 150.00m
        };
    }
}