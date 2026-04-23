using Microservices.Communication.Sample.Contracts.Messaging;

namespace Microservices.Communication.Sample.Service1.Application.Abstractions.Messaging;

public interface IService2Messenger
{
    Task<ServiceBaseResponse> RequestBaseMessageAsync(ServiceBaseRequest request, CancellationToken cancellationToken);
}
