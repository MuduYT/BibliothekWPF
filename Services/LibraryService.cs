using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bibliothek.Services;

public sealed class LibraryService(
    IDbContextFactory<BibliothekContext> contextFactory,
    ILogger<LibraryService> logger) : ILibraryService
{
    public async Task<IReadOnlyList<Buch>> GetBooksAsync(CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        return await context.Buecher
            .Include(b => b.Autor)
            .Include(b => b.Verlag)
            .AsNoTracking()
            .OrderBy(b => b.Titel)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Autor>> GetAuthorsAsync(CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        return await context.Autoren
            .Include(a => a.Buecher)
            .AsNoTracking()
            .OrderBy(a => a.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Verlag>> GetPublishersAsync(CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        return await context.Verlage
            .Include(v => v.Firmensitz)
            .Include(v => v.Buecher)
            .AsNoTracking()
            .OrderBy(v => v.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Ort>> GetPlacesAsync(CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        return await context.Orte
            .Include(o => o.Verlage)
            .AsNoTracking()
            .OrderBy(o => o.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<DashboardMetrics> GetDashboardMetricsAsync(CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        var recentBooks = await context.Buecher
            .Include(b => b.Autor)
            .Include(b => b.Verlag)
            .AsNoTracking()
            .OrderByDescending(b => b.Erscheinungsjahr)
            .ThenBy(b => b.Titel)
            .Take(5)
            .ToListAsync(cancellationToken);

        return new DashboardMetrics(
            await context.Buecher.CountAsync(cancellationToken),
            await context.Autoren.CountAsync(cancellationToken),
            await context.Verlage.CountAsync(cancellationToken),
            await context.Orte.CountAsync(cancellationToken),
            recentBooks);
    }

    public async Task SaveBookAsync(Buch book, CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        book.Autor = book.Autor is null ? null : await context.Autoren.FindAsync([book.Autor.Id], cancellationToken);
        book.Verlag = book.Verlag is null ? null : await context.Verlage.FindAsync([book.Verlag.Id], cancellationToken);

        var exists = await context.Buecher.AnyAsync(b => b.ISBN == book.ISBN, cancellationToken);
        context.Entry(book).State = exists ? EntityState.Modified : EntityState.Added;
        await context.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteBookAsync(Buch book, CancellationToken cancellationToken = default)
        => DeleteAsync<Buch>(book, cancellationToken);

    public Task SaveAuthorAsync(Autor author, CancellationToken cancellationToken = default)
        => SaveAsync(author.Id, author, cancellationToken);

    public Task DeleteAuthorAsync(Autor author, CancellationToken cancellationToken = default)
        => DeleteAsync(author, cancellationToken);

    public async Task SavePublisherAsync(Verlag publisher, CancellationToken cancellationToken = default)
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        publisher.Firmensitz = publisher.Firmensitz is null
            ? null
            : await context.Orte.FindAsync([publisher.Firmensitz.Id], cancellationToken);

        context.Entry(publisher).State = publisher.Id == 0 ? EntityState.Added : EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);
    }

    public Task DeletePublisherAsync(Verlag publisher, CancellationToken cancellationToken = default)
        => DeleteAsync(publisher, cancellationToken);

    public Task SavePlaceAsync(Ort place, CancellationToken cancellationToken = default)
        => SaveAsync(place.Id, place, cancellationToken);

    public Task DeletePlaceAsync(Ort place, CancellationToken cancellationToken = default)
        => DeleteAsync(place, cancellationToken);

    private async Task SaveAsync<TEntity>(int id, TEntity entity, CancellationToken cancellationToken)
        where TEntity : class
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        context.Entry(entity).State = id == 0 ? EntityState.Added : EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);
    }

    private async Task DeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
        where TEntity : class
    {
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        context.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
        logger.LogInformation("{Entity} wurde geloescht.", typeof(TEntity).Name);
    }
}
