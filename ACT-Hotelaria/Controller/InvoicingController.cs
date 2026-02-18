using ACT_Hotelaria.Application.UseCase.Invoicing;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ACT_Hotelaria.Controller;

public sealed class InvoicingController(IMediator mediator) : BaseController(mediator)
{
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(ACT_Hotelaria.ApiResponse.ApiResponse<RegisterInvoicingUseCaseResponse>),StatusCodes.Status201Created)]
    public async Task<IActionResult> RegisterInvoicing(RegisterInvoicingUseCaseRequest request, CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(request));
    }
    
}