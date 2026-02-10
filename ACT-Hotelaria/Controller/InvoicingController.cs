using ACT_Hotelaria.Application.UseCase.Invoicing;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ACT_Hotelaria.Controller;

public class InvoicingController(IMediator mediator) : BaseController(mediator)
{
    [HttpPost]
    public async Task<IActionResult> RegisterInvoicing(RegisterInvoicingUseCaseRequest request, CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(request));
    }
    
}