using ACT_Hotelaria.Application.UseCase.Consumption;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ACT_Hotelaria.Controller;

public class ConsumptionController(IMediator mediator) : BaseController(mediator)
{
    
    [HttpPost]
    public async Task<IActionResult> RegisterConsumption(RegisterConsumptionUseCaseRequest request, CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(request));
    }
    
}