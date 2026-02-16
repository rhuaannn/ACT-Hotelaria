using ACT_Hotelaria.ApiResponse;
using ACT_Hotelaria.Application.UseCase.Consumption;
using ACT_Hotelaria.Application.UseCase.Consumption.GetAll;
using ACT_Hotelaria.Application.UseCase.Consumption.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ACT_Hotelaria.Controller;

public class ConsumptionController(IMediator mediator) : BaseController(mediator)
{
    
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<RegisterConsumptionUseCaseResponse>),StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterConsumption(RegisterConsumptionUseCaseRequest request, CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(IEnumerable<GetAllConsumptionUseCaseResponse>),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>),StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllConsumption()
    {
        var query = new GetAllQueryConsumption();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(ACT_Hotelaria.ApiResponse.ApiResponse<GetByIdConsumptionUseCaseResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ACT_Hotelaria.ApiResponse.ApiResponse<string>),StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdConsumption(Guid id)
    {
        var query = new GetByIdQueryConsumption(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}