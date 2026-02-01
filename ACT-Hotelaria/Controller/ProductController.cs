using ACT_Hotelaria.Application.UseCase.Product;
using Microsoft.AspNetCore.Mvc;

namespace ACT_Hotelaria.Controller;

public class ProductController : BaseController
{
    private readonly RegisterProductUseCase _registerProductUseCase;

    public ProductController(RegisterProductUseCase registerProductUseCase)
    {
        _registerProductUseCase = registerProductUseCase;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ACT_Hotelaria.ApiResponse.ApiResponse<RegisterProductUseCaseResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ACT_Hotelaria.ApiResponse.ApiResponse<string>), StatusCodes.Status400BadRequest)]
    
    public async Task<IActionResult> RegisterProduct([FromBody] RegisterProductUseCaseRequest request)
    {
        var result = await _registerProductUseCase.Handle(request);
        var response = ACT_Hotelaria.ApiResponse.ApiResponse<RegisterProductUseCaseResponse>.SuccesResponse(result, 200);

        return Created(string.Empty, response);
    }
}