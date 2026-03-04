using ACT_Hotelaria.Domain.Abstract;
using ACT_Hotelaria.Domain.DomainNotification;
using ACT_Hotelaria.Domain.Exception;
using ACT_Hotelaria.Domain.Repository.ProductRepository;
using ACT_Hotelaria.Domain.Repository.Reservation;
using ACT_Hotelaria.Message;
using FluentValidation;
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
    private readonly NotificationContext _notificationContext;
    private readonly IValidator<RegisterConsumptionUseCaseRequest> _validator;

    public RegisterConsumptionUseCase(ILogger<RegisterConsumptionUseCase> logger,
                                        IReadOnlyProductRepository readOnlyProductRepository,
                                        IWriteOnlyReservationRepository writeOnlyReservationRepository,
                                        IReadOnlyReservationRepository readOnlyReservationRepository,
                                        IWriteOnlyProductRepository writeOnlyProductRepository,
                                        IUnitOfWork unitOfWork,
                                        NotificationContext notificationContext,
                                        IValidator<RegisterConsumptionUseCaseRequest> validator
                                        )
    {
        _logger = logger;
        _readOnlyProductRepository = readOnlyProductRepository;
        _writeOnlyReservationRepository = writeOnlyReservationRepository;
        _readOnlyReservationRepository = readOnlyReservationRepository;
        _writeOnlyProductRepository = writeOnlyProductRepository;
        _unitOfWork = unitOfWork;
        _notificationContext = notificationContext;
        _validator = validator;
    }

    public async Task<RegisterConsumptionUseCaseResponse> Handle(RegisterConsumptionUseCaseRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            _notificationContext.AddNotifications(validationResult);
            return null; 
        }
        var reservation = await _readOnlyReservationRepository.GetAllById(request.ReservationId);
        if (reservation == null)
        {
            _logger.LogInformation("Reserva inexistente.");
           _notificationContext.AddNotification("Consumption", ResourceMessages.ReservaNaoEncontrada);
        }
        var product = await _readOnlyProductRepository.GetById(request.ProductId);
        if (product == null)
        {
            _logger.LogInformation("Produto inexistente no estoque.");
            _notificationContext.AddNotification("Consumption", ResourceMessages.ProductNotExists);
            return null;
        }

        if (product.QtyProduct < request.Quantity)
        {
            _logger.LogInformation("Quantidade insuficiente no estoque.");
            _notificationContext.AddNotification("Consumption","Quantidade insuficiente no estoque.");
            return null;
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
