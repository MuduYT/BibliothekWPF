using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Bibliothek.Services;
using Bibliothek.ViewModels;
using Bibliothek.Views;

namespace Bibliothek;

public partial class App : Application
{
    private readonly IHost host;

    public App()
    {
        host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddLogging();
                services.AddDbContextFactory<BibliothekContext>(options =>
                    options.UseSqlServer(BibliothekContext.DefaultConnectionString));

                services.AddSingleton<IDockerService, DockerService>();
                services.AddSingleton<IDatabaseInitializer, DatabaseInitializer>();
                services.AddSingleton<ILibraryService, LibraryService>();
                services.AddSingleton<IDialogService, DialogService>();

                services.AddSingleton<DashboardViewModel>();
                services.AddSingleton<BooksViewModel>();
                services.AddSingleton<AuthorsViewModel>();
                services.AddSingleton<PublishersViewModel>();
                services.AddSingleton<PlacesViewModel>();
                services.AddSingleton<StatisticsViewModel>();
                services.AddSingleton<SettingsViewModel>();
                services.AddSingleton<MainViewModel>();
                services.AddSingleton<MainWindow>();
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        await host.StartAsync();

        try
        {
            await host.Services.GetRequiredService<IDatabaseInitializer>().InitializeAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Datenbankstart fehlgeschlagen", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        var mainWindow = host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();

        if (mainWindow.DataContext is MainViewModel mainViewModel
            && mainViewModel.CurrentViewModel is ILoadableViewModel loadable)
        {
            await loadable.LoadAsync();
        }
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await host.StopAsync();
        host.Dispose();
        base.OnExit(e);
    }
}
