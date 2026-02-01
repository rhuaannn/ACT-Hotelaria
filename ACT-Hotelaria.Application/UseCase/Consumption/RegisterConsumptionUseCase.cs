using ACT_Hotelaria.Domain.Exception;
using ACT_Hotelaria.Domain.Repository.cs.ConsumptionRepository.cs;
using ACT_Hotelaria.Domain.Repository.cs.ProductRepository;
using ACT_Hotelaria.Domain.Repository.cs.Reservation;

namespace ACT_Hotelaria.Application.UseCase.Consumption;

public class RegisterConsumptionUseCase
{
    private readonly IWriteOnlyConsumptionRepository _writeOnlyConsumptionRepository;
    private readonly IReadOnlyReservationRepository _readOnlyReservationRepository; 
    private readonly IWriteOnlyReservationRepository _writeOnlyReservationRepository;
    private readonly IReadOnlyProductRepository _readOnlyProductRepository;

    public RegisterConsumptionUseCase(IWriteOnlyConsumptionRepository writeOnlyConsumptionRepository,
                                        IReadOnlyProductRepository readOnlyProductRepository,
                                        IWriteOnlyReservationRepository writeOnlyReservationRepository,
                                        IReadOnlyReservationRepository readOnlyReservationRepository
                                        )
    {
        _writeOnlyConsumptionRepository = writeOnlyConsumptionRepository;
        _readOnlyProductRepository = readOnlyProductRepository;
        _writeOnlyReservationRepository = writeOnlyReservationRepository;
        _readOnlyReservationRepository = readOnlyReservationRepository;
    }

    public async Task<RegisterConsumptionUseCaseResponse> Handle(RegisterConsumptionUseCaseRequest request)
    {
        var reservation = await _readOnlyReservationRepository.GetAllById(request.ReservationId);
        if (reservation == null)
            throw new DomainException("Reserva inexistente");
        
        var product = await _readOnlyProductRepository.GetById(request.ProductId);
        if (product == null)
            throw new DomainException("Produto inexistente");

        if (product.QtyProduct < request.Quantity)
            throw new DomainException("Quantidade insuficiente no estoque.");
        
        reservation.AddConsumption(product, request.Quantity);
        await _writeOnlyReservationRepository.Update(reservation);   
        
        return new RegisterConsumptionUseCaseResponse
        {
            ReservationId = reservation.Id,
            ProductId = request.ProductId,
            Quantity = request.Quantity
        };
    }
}
