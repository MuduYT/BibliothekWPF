using Microsoft.EntityFrameworkCore;

namespace Bibliothek;

public class BibliothekContext : DbContext
{
    public const string DefaultConnectionString =
        "Server=localhost,1433;Database=BibliothekDB;User Id=sa;Password=Passwort123!;TrustServerCertificate=True;Encrypt=False;";

    public BibliothekContext()
    {
    }

    public BibliothekContext(DbContextOptions<BibliothekContext> options)
        : base(options)
    {
    }

    public DbSet<Buch> Buecher => Set<Buch>();
    public DbSet<Verlag> Verlage => Set<Verlag>();
    public DbSet<Autor> Autoren => Set<Autor>();
    public DbSet<Ort> Orte => Set<Ort>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            options.UseSqlServer(DefaultConnectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Buch>()
            .HasOne(b => b.Autor)
            .WithMany(a => a.Buecher)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Buch>()
            .HasOne(b => b.Verlag)
            .WithMany(v => v.Buecher)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Verlag>()
            .HasOne(v => v.Firmensitz)
            .WithMany(o => o.Verlage)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
