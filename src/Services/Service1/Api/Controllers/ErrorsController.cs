using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Communication.Sample.Service1.Api.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Route("error")]
public sealed class ErrorsController : ControllerBase
{
    [HttpGet]
    [HttpPost]
    [HttpPut]
    [HttpDelete]
    public IActionResult Handle()
    {
        var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
        if (feature?.Error is TimeoutException)
        {
            return Problem(
                title: "Service2 timeout",
                detail: "Service2 did not respond in time.",
                statusCode: StatusCodes.Status504GatewayTimeout);
        }

        return Problem(
            title: "Unhandled error",
            detail: feature?.Error.Message,
            statusCode: StatusCodes.Status500InternalServerError);
    }
}
