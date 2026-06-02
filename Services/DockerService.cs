using System.IO;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Bibliothek.Services;

public sealed class DockerService(ILogger<DockerService> logger) : IDockerService
{
    public async Task StartDatabaseAsync(CancellationToken cancellationToken = default)
    {
        var composeFile = Path.Combine(AppContext.BaseDirectory, "docker-compose.yml");
        var startInfo = new ProcessStartInfo
        {
            FileName = "docker",
            Arguments = $"compose -p bibliothek -f \"{composeFile}\" up -d",
            WorkingDirectory = AppContext.BaseDirectory,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };

        using var process = Process.Start(startInfo)
            ?? throw new InvalidOperationException("Docker konnte nicht gestartet werden.");

        var output = process.StandardOutput.ReadToEndAsync(cancellationToken);
        var error = process.StandardError.ReadToEndAsync(cancellationToken);
        await process.WaitForExitAsync(cancellationToken);

        if (process.ExitCode != 0)
        {
            var message = await error;
            logger.LogError("Docker Compose failed: {Message}", message);
            throw new InvalidOperationException($"Docker Compose Fehler: {message}");
        }

        logger.LogInformation("Docker Compose output: {Output}", await output);
        await WaitForSqlServerAsync(cancellationToken);
    }

    private static async Task WaitForSqlServerAsync(CancellationToken cancellationToken)
    {
        const int timeoutSeconds = 180;
        var startedAt = Stopwatch.StartNew();

        while (startedAt.Elapsed.TotalSeconds < timeoutSeconds)
        {
            try
            {
                await using var connection = new SqlConnection(
                    "Server=localhost,1433;Database=master;User Id=sa;Password=Passwort123!;TrustServerCertificate=True;Encrypt=False;");
                await connection.OpenAsync(cancellationToken);

                await using var command = connection.CreateCommand();
                command.CommandText = """
                    IF DB_ID(N'BibliothekDB') IS NULL
                    BEGIN
                        CREATE DATABASE BibliothekDB;
                    END
                    """;
                await command.ExecuteNonQueryAsync(cancellationToken);
                return;
            }
            catch when (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
            }
        }

        throw new TimeoutException($"SQL Server war nach {timeoutSeconds} Sekunden nicht erreichbar.");
    }
}
