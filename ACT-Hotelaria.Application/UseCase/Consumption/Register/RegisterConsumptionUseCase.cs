using ACT_Hotelaria.Domain.Exception;
using ACT_Hotelaria.Domain.Repository.ConsumptionRepository.cs;
using ACT_Hotelaria.Domain.Repository.ProductRepository;
using ACT_Hotelaria.Domain.Repository.Reservation;
using ACT_Hotelaria.Message;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ACT_Hotelaria.Application.UseCase.Consumption;

public class RegisterConsumptionUseCase : IRequestHandler<RegisterConsumptionUseCaseRequest, RegisterConsumptionUseCaseResponse>
{
    private readonly ILogger<RegisterConsumptionUseCase> _logger;
    private readonly IReadOnlyReservationRepository _readOnlyReservationRepository; 
    private readonly IWriteOnlyReservationRepository _writeOnlyReservationRepository;
    private readonly IReadOnlyProductRepository _readOnlyProductRepository;
    private readonly IWriteOnlyProductRepository _writeOnlyProductRepository;

    public RegisterConsumptionUseCase(ILogger<RegisterConsumptionUseCase> logger,
                                        IReadOnlyProductRepository readOnlyProductRepository,
                                        IWriteOnlyReservationRepository writeOnlyReservationRepository,
                                        IReadOnlyReservationRepository readOnlyReservationRepository,
                                        IWriteOnlyProductRepository writeOnlyProductRepository
                                        )
    {
        _logger = logger;
        _readOnlyProductRepository = readOnlyProductRepository;
        _writeOnlyReservationRepository = writeOnlyReservationRepository;
        _readOnlyReservationRepository = readOnlyReservationRepository;
        _writeOnlyProductRepository = writeOnlyProductRepository;
    }

    public async Task<RegisterConsumptionUseCaseResponse> Handle(RegisterConsumptionUseCaseRequest request, CancellationToken cancellationToken)
    {
        var reservation = await _readOnlyReservationRepository.GetAllById(request.ReservationId);
        if (reservation == null)
            throw new DomainException(ResourceMessages.CheckinObrigatorio);
        
        var product = await _readOnlyProductRepository.GetById(request.ProductId);
        if (product == null)
        {
            _logger.LogError("Produto inexistente no estoque.");
            throw new DomainException("Produto inexistente");
            
        }

        if (product.QtyProduct < request.Quantity)
            throw new DomainException("Quantidade insuficiente no estoque.");
        
        reservation.AddConsumption(product, request.Quantity);
        await _writeOnlyReservationRepository.Update(reservation);
         _writeOnlyProductRepository.Update(product);
        _logger.LogInformation($"Consumo de {request.Quantity} unidades do produto {product.Name} realizado com sucesso!");
        return new RegisterConsumptionUseCaseResponse
        {
            ReservationId = reservation.Id,
            ProductId = request.ProductId,
            Quantity = request.Quantity
        };
    }
}
