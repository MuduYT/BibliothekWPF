namespace Bibliothek.Services;

public interface IDockerService
{
    Task StartDatabaseAsync(CancellationToken cancellationToken = default);
}
