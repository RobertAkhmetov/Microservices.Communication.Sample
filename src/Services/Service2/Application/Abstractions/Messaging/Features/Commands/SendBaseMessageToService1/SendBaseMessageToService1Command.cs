using MediatR;

namespace Service2.Application.Abstractions.Messaging.Features.Commands.SendBaseMessageToService1;

public sealed record SendBaseMessageToService1Command(string Message) : IRequest<SendBaseMessageToService1Result>;
