using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SeCumple.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class FollowUpController(IMediator mediator) : ControllerBase
{
    
}