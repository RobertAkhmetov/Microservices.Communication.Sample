using MediatR;
using Microservices.Communication.Sample.Service1.Api.Contracts;
using Microservices.Communication.Sample.Service1.Application.Features.SendBaseMessageToService2;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Communication.Sample.Service1.Api.Controllers;

[ApiController]
[Route("api/messages")]
public sealed class MessagesController(ISender sender) : ControllerBase
{
    [HttpPost("service2/base")]
    [ProducesResponseType<SendBaseMessageResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status504GatewayTimeout)]
    public async Task<IActionResult> SendBaseMessage(
        [FromBody] SendBaseMessageRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await sender.Send(new SendBaseMessageToService2Command(request.Message), cancellationToken);
            return Ok(new SendBaseMessageResponse(
                result.RequestId,
                result.RequestMessage,
                result.ResponseMessage,
                result.RequestedAtUtc,
                result.RespondedAtUtc));
        }
        catch (ArgumentException)
        {
            return ValidationProblem("Message is required.");
        }
        catch (TimeoutException)
        {
            return Problem(
                title: "Service2 timeout",
                detail: "Service2 did not respond in time.",
                statusCode: StatusCodes.Status504GatewayTimeout);
        }
    }
}
