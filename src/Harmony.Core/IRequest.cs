namespace Harmony.Core;

internal interface IRequest
{
    void Execute();
    T Execute<T>();
    Task ExecuteAsync(CancellationToken cancellationToken = default);
    Task<T> ExecuteAsync<T>(CancellationToken cancellationToken = default);
}