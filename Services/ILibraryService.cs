namespace Bibliothek.Services;

public interface ILibraryService
{
    Task<IReadOnlyList<Buch>> GetBooksAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Autor>> GetAuthorsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Verlag>> GetPublishersAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Ort>> GetPlacesAsync(CancellationToken cancellationToken = default);
    Task<DashboardMetrics> GetDashboardMetricsAsync(CancellationToken cancellationToken = default);

    Task SaveBookAsync(Buch book, CancellationToken cancellationToken = default);
    Task DeleteBookAsync(Buch book, CancellationToken cancellationToken = default);
    Task SaveAuthorAsync(Autor author, CancellationToken cancellationToken = default);
    Task DeleteAuthorAsync(Autor author, CancellationToken cancellationToken = default);
    Task SavePublisherAsync(Verlag publisher, CancellationToken cancellationToken = default);
    Task DeletePublisherAsync(Verlag publisher, CancellationToken cancellationToken = default);
    Task SavePlaceAsync(Ort place, CancellationToken cancellationToken = default);
    Task DeletePlaceAsync(Ort place, CancellationToken cancellationToken = default);
}

public sealed record DashboardMetrics(
    int BookCount,
    int AuthorCount,
    int PublisherCount,
    int PlaceCount,
    IReadOnlyList<Buch> RecentBooks);
