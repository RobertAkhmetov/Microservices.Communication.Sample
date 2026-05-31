using MediatR;

namespace Service2.Application.Abstractions.Messaging.Features.Queriess;

public sealed record GetMessagesQuery : IRequest<string>;

internal sealed class GetMessagesQueryHandler(IConsumerService consumerService)
    : IRequestHandler<GetMessagesQuery, string>
{

    public async Task<string> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
    {
        return await consumerService.GetMessagesAsync(cancellationToken);
    }
}
