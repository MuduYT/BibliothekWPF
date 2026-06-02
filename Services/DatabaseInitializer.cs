using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bibliothek.Services;

public sealed class DatabaseInitializer(
    IDockerService dockerService,
    IDbContextFactory<BibliothekContext> contextFactory,
    ILogger<DatabaseInitializer> logger) : IDatabaseInitializer
{
    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await dockerService.StartDatabaseAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Docker database startup failed");
            return;
        }

        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);
        try
        {
            await context.Database.MigrateAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Migration failed");
            return;
        }

        try
        {
            await SeedAsync(context, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Database seeding failed");
        }
    }

    private async Task SeedAsync(BibliothekContext context, CancellationToken cancellationToken)
    {
        if (await context.Orte.AnyAsync(cancellationToken)
            || await context.Verlage.AnyAsync(cancellationToken)
            || await context.Autoren.AnyAsync(cancellationToken)
            || await context.Buecher.AnyAsync(cancellationToken))
        {
            return;
        }

        var berlin = new Ort { Name = "Berlin", Postleitzahl = 10115 };
        var hamburg = new Ort { Name = "Hamburg", Postleitzahl = 20095 };
        var klagenfurt = new Ort { Name = "Klagenfurt", Postleitzahl = 9020 };

        var verlag1 = new Verlag { Name = "Nachbar", Firmensitz = berlin, Telefonnummer = "0123-45678", Email = "info@nachbar.at" };
        var verlag2 = new Verlag { Name = "Gute Pucher", Firmensitz = hamburg, Telefonnummer = "0987-65432", Email = "gute@pucher.de" };
        var verlag3 = new Verlag { Name = "Puecherwurm", Firmensitz = klagenfurt, Telefonnummer = "0463-12345", Email = "wurm@puecher.at" };

        var autor1 = new Autor { Name = "Roland Hraschan", Jahrgang = 1990 };
        var autor2 = new Autor { Name = "Chuck Norris", Jahrgang = 1940 };
        var autor3 = new Autor { Name = "Timmy", Jahrgang = 2010 };

        context.AddRange(
            berlin, hamburg, klagenfurt,
            verlag1, verlag2, verlag3,
            autor1, autor2, autor3,
            new Buch { Titel = "Das Leben des Roland Hraschan", ISBN = "978-1-234-56789-0", AnzahlSeiten = 200, Erscheinungsjahr = 2024, Verlag = verlag2, Autor = autor1 },
            new Buch { Titel = "Mit Godmode durch die Galaxy", ISBN = "978-0-987-65432-1", AnzahlSeiten = 1337, Erscheinungsjahr = 2023, Verlag = verlag1, Autor = autor2 },
            new Buch { Titel = "5 Mal fressen pro Tag das ist was ich mag", ISBN = "978-3-111-22233-4", AnzahlSeiten = 50, Erscheinungsjahr = 2025, Verlag = verlag3, Autor = autor3 });

        await context.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Seed-Daten wurden angelegt.");
    }
}
