using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ACT_Hotelaria.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class BaseController(IMediator mediator) : ControllerBase
{
    protected IMediator _mediator = mediator;
}