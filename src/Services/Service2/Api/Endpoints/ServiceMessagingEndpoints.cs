using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service2.Application.Abstractions.Messaging.Features.Queriess;

namespace Service2.Api.Endpoints;

public static class ServiceMessagingEndpoints
{
    public static void MapServiceMessagingEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/service1");

        group.MapPost("/base", SendMessageToService1Async).WithName("Send message to service 1");
        group.MapGet("/messages", GetMessagesFromQueryAsync);

    }

    private static async Task SendMessageToService1Async([FromBody] SendBaseMessageRequest request, ISender sender, CancellationToken token)
    {
        try
        {
            //var result = await sender
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static async Task<string> GetMessagesFromQueryAsync(ISender sender, CancellationToken token)
    {
        var result = await sender.Send(new GetMessagesQuery(), token);

        return "test";
    }
}
