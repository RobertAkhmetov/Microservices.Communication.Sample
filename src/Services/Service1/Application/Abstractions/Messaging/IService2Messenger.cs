using Contracts.Messaging;

namespace Service1.Application.Abstractions.Messaging;

public interface IService2Messenger
{
    Task<ServiceBaseResponse> RequestBaseMessageAsync(ServiceBaseRequest request, CancellationToken cancellationToken);
}
