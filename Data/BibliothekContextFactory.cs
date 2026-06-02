using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Bibliothek;

public sealed class BibliothekContextFactory : IDesignTimeDbContextFactory<BibliothekContext>
{
    public BibliothekContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<BibliothekContext>()
            .UseSqlServer(BibliothekContext.DefaultConnectionString)
            .Options;

        return new BibliothekContext(options);
    }
}
