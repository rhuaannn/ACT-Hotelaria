using ACT_Hotelaria.Domain.Abstract;
using ACT_Hotelaria.Domain.DomainNotification;
using ACT_Hotelaria.Domain.Exception;
using ACT_Hotelaria.Domain.Repository.InvoicingRepository;
using ACT_Hotelaria.Domain.Repository.Reservation;
using ACT_Hotelaria.Message;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ACT_Hotelaria.Application.UseCase.Invoicing;

public class RegisterInvoicingUseCase : IRequestHandler<RegisterInvoicingUseCaseRequest, RegisterInvoicingUseCaseResponse>
{
    private readonly IWriteOnlyInvoiceRepository _writeOnlyInvoiceRepository;
    private readonly IReadOnlyReservationRepository _readOnlyReservationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RegisterInvoicingUseCase> _logger;
    private readonly IValidator<RegisterInvoicingUseCaseRequest> _validator;
    private readonly NotificationContext _notificationContext;
    
    public RegisterInvoicingUseCase(
        IWriteOnlyInvoiceRepository writeOnlyInvoiceRepository,
        ILogger<RegisterInvoicingUseCase> logger,
        IUnitOfWork unitOfWork,
        IReadOnlyReservationRepository readOnlyReservationRepository,
        IValidator<RegisterInvoicingUseCaseRequest> validator,
        NotificationContext notificationContext
        )
    {
        _writeOnlyInvoiceRepository = writeOnlyInvoiceRepository;
        _readOnlyReservationRepository = readOnlyReservationRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _notificationContext = notificationContext;
    }

    public async Task<RegisterInvoicingUseCaseResponse> Handle(RegisterInvoicingUseCaseRequest request, CancellationToken cancellationToken)
    {

        var reservation = await _readOnlyReservationRepository.GetAllById(request.ReservationId);

        if (reservation == null)
        {
            _logger.LogInformation("Reserva inexistente.");
           _notificationContext.AddNotification("Reserva:",ResourceMessages.ReservaNaoEncontrada);
           return null;
        }

        if (_notificationContext.HasNotifications)
        {
            return null;
        }

        var invoice = Domain.Entities.Invoicing.Create(reservation);
        
        await _writeOnlyInvoiceRepository.Add(invoice);
        await _unitOfWork.CommitAsync(cancellationToken);
        _logger.LogInformation($"Fatura {invoice.Id} gerada com sucesso!");

        return new RegisterInvoicingUseCaseResponse
        {
            InvoiceId = invoice.Id,
            TotalRoom = invoice.TotalRoomValue,
            TotalConsumption = invoice.TotalConsumptionValue,
            GrandTotal = invoice.ValueTotal,
            IssueDate = invoice.IssueDate
        };
    }
}