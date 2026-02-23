using ACT_Hotelaria.Domain.Abstract;
using ACT_Hotelaria.Domain.Exception;
using ACT_Hotelaria.Domain.Notification;
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
    private readonly IUnitOfWork _unitOfWork;
    private readonly Domain.Interface.INotification _notification;

    public RegisterConsumptionUseCase(ILogger<RegisterConsumptionUseCase> logger,
                                        IReadOnlyProductRepository readOnlyProductRepository,
                                        IWriteOnlyReservationRepository writeOnlyReservationRepository,
                                        IReadOnlyReservationRepository readOnlyReservationRepository,
                                        IWriteOnlyProductRepository writeOnlyProductRepository,
                                        IUnitOfWork unitOfWork,
                                        Domain.Interface.INotification notification
                                        )
    {
        _logger = logger;
        _readOnlyProductRepository = readOnlyProductRepository;
        _writeOnlyReservationRepository = writeOnlyReservationRepository;
        _readOnlyReservationRepository = readOnlyReservationRepository;
        _writeOnlyProductRepository = writeOnlyProductRepository;
        _unitOfWork = unitOfWork;
        _notification = notification;
    }

    public async Task<RegisterConsumptionUseCaseResponse> Handle(RegisterConsumptionUseCaseRequest request, CancellationToken cancellationToken)
    {
        var reservation = await _readOnlyReservationRepository.GetAllById(request.ReservationId);
        if (reservation == null)
        {
            _logger.LogInformation("Reserva inexistente.");
            _notification.Handle(new Notification(ResourceMessages.ReservaNaoEncontrada));
        }
        
        var product = await _readOnlyProductRepository.GetById(request.ProductId);
        if (product == null)
        {
            _logger.LogInformation("Produto inexistente no estoque.");
            _notification.Handle(new Notification("Produto inexistente no estoque."));
            
        }

        if (product.QtyProduct < request.Quantity)
        {
            _logger.LogInformation("Quantidade insuficiente no estoque.");
            _notification.Handle(new Notification("Quantidade insuficiente no estoque."));
        }
        
        reservation.AddConsumption(product, request.Quantity);
        await _writeOnlyReservationRepository.Update(reservation);
         _writeOnlyProductRepository.Update(product);
        _logger.LogInformation($"Consumo de {request.Quantity} unidades do produto {product.Name} realizado com sucesso!");
        await _unitOfWork.CommitAsync(cancellationToken);
        return new RegisterConsumptionUseCaseResponse
        {
            ReservationId = reservation.Id,
            ProductId = request.ProductId,
            Quantity = request.Quantity
        };
    }
}
