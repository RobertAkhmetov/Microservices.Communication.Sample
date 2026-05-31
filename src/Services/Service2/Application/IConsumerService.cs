namespace Service2.Application;

public interface IConsumerService : IDisposable
{
    Task<string> GetMessagesAsync(CancellationToken cancellationToken);
}
