using MediatR;

namespace Microservices.Communication.Sample.Service1.Application.Features.SendBaseMessageToService2;

public sealed record SendBaseMessageToService2Command(string Message) : IRequest<SendBaseMessageToService2Result>;
