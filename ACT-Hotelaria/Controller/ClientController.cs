using ACT_Hotelaria.ApiResponse;
using ACT_Hotelaria.Application.UseCase.Client;
using ACT_Hotelaria.Application.UseCase.Client.GetAll;
using ACT_Hotelaria.Application.UseCase.Client.GetById;
using ACT_Hotelaria.Domain.Repository.ClientRepository;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ACT_Hotelaria.Controller;

public sealed class ClientController(IMediator mediator) : BaseController(mediator)
{
    
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<RegisterClientUseCaseResponse>),StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterClient([FromBody] RegisterClientUseCaseRequest request)
    {
        var response = await _mediator.Send(request);
        return CreatedAtAction(nameof(RegisterClient), new { id = response }, response);
        
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(ACT_Hotelaria.ApiResponse.ApiResponse<GetAllClientResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ACT_Hotelaria.ApiResponse.ApiResponse<string>),StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllClients()
    {
        var query = new GetAllQueryClientUseCase();
        var clients = await _mediator.Send(query);
        var response = ACT_Hotelaria.ApiResponse.ApiResponse<IEnumerable<GetAllClientResponse>>
            .SuccesResponse(clients, 200);        
        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ACT_Hotelaria.ApiResponse.ApiResponse<GetByIdClientUseCaseResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ACT_Hotelaria.ApiResponse.ApiResponse<string>),StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetByIdQueryClientUseCase(id);
        var result = await _mediator.Send(query);
        var response = ACT_Hotelaria.ApiResponse.ApiResponse<GetByIdClientUseCaseResponse>.SuccesResponse(result, 200);
        return Ok(response);
    }
    
}
