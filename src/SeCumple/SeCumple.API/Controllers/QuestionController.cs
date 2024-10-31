using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SeCumple.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class QuestionController(IMediator mediator) : ControllerBase
{
    // [HttpGet]
    // [Route("~/api/seguimiento/ListarLineamiento", Name = "GetGuideLineByAxisId")]
    // public async Task<IActionResult> GetGuideLineByAxisId(int id)
    // {
    //     var query = new GetGuidelLinesByAxisIdQuery { AxisId = id };
    //     return Ok(await mediator.Send(query));
    // }
    //
    // [HttpPost("~/api/seguimiento/RegistrarPregunta", Name = "CreateQuestion")]
    // public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionCommand request)
    // {
    //     return Ok(await mediator.Send(request));
    // }
    //
    // [HttpPost("~/api/seguimiento/ActulizarLineamiento", Name = "UpdateGuideLine")]
    // public async Task<IActionResult> UpdateGuideLine([FromBody] UpdateGuidelineCommand request)
    // {
    //     return Ok(await mediator.Send(request));
    // }
}