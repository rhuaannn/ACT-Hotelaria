using ACT_Hotelaria.Application.UseCase.Consumption;
using Microsoft.AspNetCore.Mvc;

namespace ACT_Hotelaria.Controller;

public class ConsumptionController : BaseController
{
    private readonly RegisterConsumptionUseCase _registerConsumptionUseCase;

    public ConsumptionController(RegisterConsumptionUseCase registerConsumptionUseCase)
    {
        _registerConsumptionUseCase = registerConsumptionUseCase;
    }
    
    [HttpPost]
    public async Task<IActionResult> RegisterConsumption([FromBody] RegisterConsumptionUseCaseRequest request)
    {
        return Ok(await _registerConsumptionUseCase.Handle(request));
    }
    
}