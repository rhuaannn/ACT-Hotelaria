using MediatR;

namespace ACT_Hotelaria.Application.UseCase.Invoicing;

public class RegisterInvoicingUseCaseRequest : IRequest<RegisterInvoicingUseCaseResponse>
{
    public Guid ReservationId { get; set; }
}