using ACT_Hotelaria.Application.UseCase.Client;
using ACT_Hotelaria.Application.UseCase.Client.GetAll;
using ACT_Hotelaria.Domain.Repository.ClientRepository;
using Microsoft.AspNetCore.Mvc;

namespace ACT_Hotelaria.Controller;

public class ClientController : BaseController
{
    private readonly RegisterClientUseCase _registerClientUseCase;
    private readonly GetAllClientUseCase _getAllClientUseCase;

    public ClientController(RegisterClientUseCase registerClientUseCase, GetAllClientUseCase getAllClientUseCase)
    {
        _registerClientUseCase = registerClientUseCase;
        _getAllClientUseCase = getAllClientUseCase;
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
    public async Task<IActionResult> GetAllClients()
    {
        var clients = await _getAllClientUseCase.Handle();
        return Ok(clients);       
    }
    
}