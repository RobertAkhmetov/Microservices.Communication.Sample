using Contracts.Messaging;

namespace Service2.Application.Abstractions.Messaging;

public interface IService1Messenger
{
    Task<ServiceBaseResponse> RequestBaseMessageAsync(ServiceBaseRequest request, CancellationToken cancellationToken);
}
