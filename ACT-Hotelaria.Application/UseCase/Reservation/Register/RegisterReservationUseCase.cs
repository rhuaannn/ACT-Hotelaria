using ACT_Hotelaria.Domain.Enum;
using ACT_Hotelaria.Domain.Exception;
using ACT_Hotelaria.Domain.Repository.ClientRepository;
using ACT_Hotelaria.Domain.Repository.cs.Reservation;
using ACT_Hotelaria.Message;

namespace ACT_Hotelaria.Application.UseCase.Reservation;

public class RegisterReservationUseCase
{
    private readonly IWriteOnlyReservationRepository _writeOnlyReservationRepository;
    private readonly IReadOnlyReservationRepository _readOnlyReservationRepository;
    private readonly IReadOnlyClientRepository _readOnlyClientRepository;

    public RegisterReservationUseCase(
        IReadOnlyClientRepository readOnlyClientRepository, 
        IWriteOnlyReservationRepository writeOnlyReservationRepository, 
        IReadOnlyReservationRepository readOnlyReservationRepository)
    {
        _readOnlyClientRepository = readOnlyClientRepository;
        _writeOnlyReservationRepository = writeOnlyReservationRepository;
        _readOnlyReservationRepository = readOnlyReservationRepository;
    }

    public async Task<RegisterReservationUseCaseResponse> Handle(RegisterReservationUseCaseRequest request)
    {
        var client = await _readOnlyClientRepository.Exists(request.ClientId);
        if (!client)
        {
            throw new NotFoundException(ResourceMessages.ClienteNaoEncontrado);
        }
        var existsCheckin = await _readOnlyReservationRepository.ExistsCheckin(request.CheckIn);
        var existsCheckout = await _readOnlyReservationRepository.ExistsCheckout(request.CheckOut);
        
        if (existsCheckin || existsCheckout)
        {
            throw new DomainException(ResourceMessages.ReservaJaCadastrada);
        }
        
        var reservation = Domain.Entities.Reservation.Create(
            request.Type, 
            request.CheckIn, 
            request.CheckOut, 
            request.ClientId,
            request.AgreedDailyRate);
        
        await _writeOnlyReservationRepository.Add(reservation);
        
        return new RegisterReservationUseCaseResponse{
            Id = request.ClientId,
            CheckIn = request.CheckIn,
            CheckOut = request.CheckOut,
            DailyValue = reservation.AgreedDailyRate
            };
    }
    
}