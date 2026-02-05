using ACT_Hotelaria.Application.UseCase.Product;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ACT_Hotelaria.Controller;

public class ProductController(IMediator mediator) : BaseController(mediator)
{

    [HttpPost]
    [ProducesResponseType(typeof(ACT_Hotelaria.ApiResponse.ApiResponse<RegisterProductUseCaseResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ACT_Hotelaria.ApiResponse.ApiResponse<string>), StatusCodes.Status400BadRequest)]
    
    public async Task<IActionResult> RegisterProduct(RegisterProductUseCaseRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request);
        var response = ACT_Hotelaria.ApiResponse.ApiResponse<RegisterProductUseCaseResponse>.SuccesResponse(result, 200);
        return Created(string.Empty, response);
    }
}