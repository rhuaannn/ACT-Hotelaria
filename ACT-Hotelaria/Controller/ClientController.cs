using ACT_Hotelaria.Application.UseCase.Client;
using Microsoft.AspNetCore.Mvc;

namespace ACT_Hotelaria.Controller;

public class ClientController : BaseController
{
    private readonly RegisterClientUseCase _registerClientUseCase;

    public ClientController(RegisterClientUseCase registerClientUseCase)
    {
        _registerClientUseCase = registerClientUseCase;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterClient([FromBody] RegisterClientUseCaseRequest request)
    {
        var response = await _registerClientUseCase.Handle(request);
        return CreatedAtAction(nameof(RegisterClient), new { id = response }, response);    }
}