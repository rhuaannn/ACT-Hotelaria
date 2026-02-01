using ACT_Hotelaria.Application.UseCase.Invoicing;
using Microsoft.AspNetCore.Mvc;

namespace ACT_Hotelaria.Controller;

public class InvoicingController : BaseController
{
    private readonly RegisterInvoicingUseCase _registerInvoicingUseCase;

    public InvoicingController(RegisterInvoicingUseCase registerInvoicingUseCase)
    {
        _registerInvoicingUseCase = registerInvoicingUseCase;
    }
    
    [HttpPost]
    public async Task<IActionResult> RegisterInvoicing([FromBody] RegisterInvoicingUseCaseRequest request)
    {
        return Ok(await _registerInvoicingUseCase.Handle(request));
    }
    
}