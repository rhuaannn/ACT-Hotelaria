using ACT_Hotelaria.Application.UseCase.Client;
using ACT_Hotelaria.Application.UseCase.Client.GetAll;
using ACT_Hotelaria.Application.UseCase.Client.GetById;
using ACT_Hotelaria.Domain.Repository.ClientRepository;
using Microsoft.AspNetCore.Mvc;

namespace ACT_Hotelaria.Controller;

public class ClientController : BaseController
{
    private readonly RegisterClientUseCase _registerClientUseCase;
    private readonly GetAllClientUseCase _getAllClientUseCase;
    private readonly GetByIdClientUseCase _getByIdClientUseCase;

    public ClientController(RegisterClientUseCase registerClientUseCase, 
        GetByIdClientUseCase getByIdClientUseCase,
        GetAllClientUseCase getAllClientUseCase
       )
    {
        _registerClientUseCase = registerClientUseCase;
        _getAllClientUseCase = getAllClientUseCase;
        _getByIdClientUseCase = getByIdClientUseCase;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterClient([FromBody] RegisterClientUseCaseRequest request)
    {
        var response = await _registerClientUseCase.Handle(request);
        return CreatedAtAction(nameof(RegisterClient), new { id = response }, response);
        
    }

    [HttpGet]
    [ProducesResponseType(typeof(ACT_Hotelaria.ApiResponse.ApiResponse<GetAllClientResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ACT_Hotelaria.ApiResponse.ApiResponse<string>),StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllClients()
    {
        var clients = await _getAllClientUseCase.Handle();
        var response = ACT_Hotelaria.ApiResponse.ApiResponse<IEnumerable<GetAllClientResponse>>
            .SuccesResponse(clients);        
        return Ok(response);
    }

    [HttpGet("id")]
    [ProducesResponseType(typeof(ACT_Hotelaria.ApiResponse.ApiResponse<GetByIdClientUseCaseResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ACT_Hotelaria.ApiResponse.ApiResponse<string>),StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var client = await _getByIdClientUseCase.Handle(id);
        var response = ACT_Hotelaria.ApiResponse.ApiResponse<GetByIdClientUseCaseResponse>.SuccesResponse(client);
        return Ok(response);
    }
}