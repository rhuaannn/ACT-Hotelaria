using ACT_Hotelaria.Domain.Enum;
using ACT_Hotelaria.Domain.Exception;
using ACT_Hotelaria.Domain.Repository.ClientRepository;
 using ACT_Hotelaria.Domain.Repository.Reservation;
 using ACT_Hotelaria.Domain.Repository.RoomRepository;
using ACT_Hotelaria.Message;
using MediatR;

namespace ACT_Hotelaria.Application.UseCase.Reservation;

public class RegisterReservationUseCase : IRequestHandler<RegisterReservationUseCaseRequest, RegisterReservationUseCaseResponse>
{
    private readonly IWriteOnlyReservationRepository _writeOnlyReservationRepository;
    private readonly IReadOnlyClientRepository _readOnlyClientRepository;
    private readonly IReadOnlyRoomRepository _readOnlyRoomRepository;

    public RegisterReservationUseCase(
        IReadOnlyClientRepository readOnlyClientRepository, 
        IWriteOnlyReservationRepository writeOnlyReservationRepository, 
        IReadOnlyRoomRepository readOnlyRoomRepository)
    {
        _readOnlyClientRepository = readOnlyClientRepository;
        _writeOnlyReservationRepository = writeOnlyReservationRepository;
        _readOnlyRoomRepository = readOnlyRoomRepository;
    }

    public async Task<RegisterReservationUseCaseResponse> Handle(RegisterReservationUseCaseRequest request, CancellationToken cancellationToken)
    {
        var client = await _readOnlyClientRepository.Exists(request.ClientId);
        var existsReservationClientPeriod = await _readOnlyClientRepository.ExistsCheckinPeriod(request.CheckIn, request.CheckOut);
        if (existsReservationClientPeriod)
        {
            throw new DomainException("Já existe reservar para o cliente no período informado");
        }
        if (!client)
        {
            throw new NotFoundException(ResourceMessages.ClienteNaoEncontrado);
        }

        var existsRoom = await _readOnlyRoomRepository.Exists(request.RoomId);
        
        if (!existsRoom)
        {
            throw new DomainException("RoomId não existe!");
        }
        
        var room = await _readOnlyRoomRepository.GetById(request.RoomId);
        var occupiedCount = await _readOnlyRoomRepository.GetOccupancyCountAsync(
            request.RoomId, 
            request.CheckIn, 
            request.CheckOut
        );

        if ((occupiedCount + 1) > room.QtyRoom) 
        {
            throw new DomainException("Não há disponibilidade para este período.");
        }

        var reservation = Domain.Entities.Reservation.Create(
            request.RoomId,
            request.CheckIn, 
            request.CheckOut, 
            request.ClientId,
            request.AgreedDailyRate);
        
        await _writeOnlyReservationRepository.Add(reservation);
        
        return new RegisterReservationUseCaseResponse{
            RoomId = request.RoomId,
            Id = request.ClientId,
            CheckIn = request.CheckIn,
            CheckOut = request.CheckOut,
            DailyValue = reservation.AgreedDailyRate
            };
    }
}