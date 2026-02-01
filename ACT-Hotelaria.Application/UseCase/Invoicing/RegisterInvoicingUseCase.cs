using ACT_Hotelaria.Domain.Exception;
using ACT_Hotelaria.Domain.Repository.cs.InvoicingRepository;
using ACT_Hotelaria.Domain.Repository.cs.Reservation;
using ACT_Hotelaria.Message;
using Azure.Core;

namespace ACT_Hotelaria.Application.UseCase.Invoicing;

public class RegisterInvoicingUseCase
{
    private readonly IWriteOnlyInvoiceRepository _writeOnlyInvoiceRepository;
    private readonly IReadOnlyReservationRepository _readOnlyReservationRepository;
    
    public RegisterInvoicingUseCase(
        IWriteOnlyInvoiceRepository writeOnlyInvoiceRepository,
        IReadOnlyReservationRepository readOnlyReservationRepository)
    {
        _writeOnlyInvoiceRepository = writeOnlyInvoiceRepository;
        _readOnlyReservationRepository = readOnlyReservationRepository;
    }

    public async Task<RegisterInvoicingUseCaseResponse> Handle(RegisterInvoicingUseCaseRequest request)
    {
        var reservation = await _readOnlyReservationRepository.GetAllById(request.ReservationId);

        if (reservation == null)
            throw new DomainException(ResourceMessages.ReservaNaoEncontrada);

        var invoice = Domain.Entities.Invoicing.Create(reservation);
        
        await _writeOnlyInvoiceRepository.Add(invoice);

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