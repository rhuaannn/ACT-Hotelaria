using ACT_Hotelaria.ApiResponse;
using ACT_Hotelaria.Application.UseCase.Product;
using ACT_Hotelaria.Application.UseCase.Product.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ACT_Hotelaria.Controller;

public sealed class ProductController(IMediator mediator) : BaseController(mediator)
{

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<RegisterProductUseCaseResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
    
    public async Task<IActionResult> RegisterProduct(RegisterProductUseCaseRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request);
        var response = ApiResponse<RegisterProductUseCaseResponse>.SuccesResponse(result, 201);
        return Created(string.Empty, response);
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<GetAllProductUseCaseResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>),StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken = default)
    {
        var query = new GetAllQueryProduct();
        var products = await _mediator.Send(query);
        var response = ApiResponse<IEnumerable<GetAllProductUseCaseResponse>>
            .SuccesResponse(products, 200);
        return Ok(response);
    }
}