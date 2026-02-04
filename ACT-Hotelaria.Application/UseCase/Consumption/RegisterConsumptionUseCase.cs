using ACT_Hotelaria.Domain.Exception;
using ACT_Hotelaria.Domain.Repository.ConsumptionRepository.cs;
using ACT_Hotelaria.Domain.Repository.ProductRepository;
using ACT_Hotelaria.Domain.Repository.Reservation;
using ACT_Hotelaria.Message;

namespace ACT_Hotelaria.Application.UseCase.Consumption;

public class RegisterConsumptionUseCase
{
    private readonly IWriteOnlyConsumptionRepository _writeOnlyConsumptionRepository;
    private readonly IReadOnlyReservationRepository _readOnlyReservationRepository; 
    private readonly IWriteOnlyReservationRepository _writeOnlyReservationRepository;
    private readonly IReadOnlyProductRepository _readOnlyProductRepository;
    private readonly IWriteOnlyProductRepository _writeOnlyProductRepository;

    public RegisterConsumptionUseCase(IWriteOnlyConsumptionRepository writeOnlyConsumptionRepository,
                                        IReadOnlyProductRepository readOnlyProductRepository,
                                        IWriteOnlyReservationRepository writeOnlyReservationRepository,
                                        IReadOnlyReservationRepository readOnlyReservationRepository,
                                        IWriteOnlyProductRepository writeOnlyProductRepository
                                        )
    {
        _writeOnlyConsumptionRepository = writeOnlyConsumptionRepository;
        _readOnlyProductRepository = readOnlyProductRepository;
        _writeOnlyReservationRepository = writeOnlyReservationRepository;
        _readOnlyReservationRepository = readOnlyReservationRepository;
        _writeOnlyProductRepository = writeOnlyProductRepository;
    }

    public async Task<RegisterConsumptionUseCaseResponse> Handle(RegisterConsumptionUseCaseRequest request)
    {
        var reservation = await _readOnlyReservationRepository.GetAllById(request.ReservationId);
        if (reservation == null)
            throw new DomainException(ResourceMessages.CheckinObrigatorio);
        
        var product = await _readOnlyProductRepository.GetById(request.ProductId);
        if (product == null)
            throw new DomainException("Produto inexistente");

        if (product.QtyProduct < request.Quantity)
            throw new DomainException("Quantidade insuficiente no estoque.");
        
        reservation.AddConsumption(product, request.Quantity);
        await _writeOnlyReservationRepository.Update(reservation);
         _writeOnlyProductRepository.Update(product);
        
        return new RegisterConsumptionUseCaseResponse
        {
            ReservationId = reservation.Id,
            ProductId = request.ProductId,
            Quantity = request.Quantity
        };
    }
}
