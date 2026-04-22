using Microservices.Communication.Sample.Contracts.Messaging;

namespace Microservices.Communication.Sample.Service2.Application.Abstractions.Messaging;

public interface IService2Messenger
{
    Task<Service2BaseResponse> RequestBaseMessageAsync(Service2BaseRequest request, CancellationToken cancellationToken);
}
