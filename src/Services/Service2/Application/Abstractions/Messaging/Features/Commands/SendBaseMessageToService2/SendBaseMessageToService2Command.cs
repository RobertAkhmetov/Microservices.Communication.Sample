using MediatR;

namespace Microservices.Communication.Sample.Service2.Application.Abstractions.Messaging.Features.Commands.SendBaseMessageToService2;

public sealed record SendBaseMessageToService2Command(string Message) : IRequest<SendBaseMessageToService2Result>;
