using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Bibliothek.Services;

namespace Bibliothek.ViewModels;

public sealed partial class StatisticsViewModel(ILibraryService libraryService) : ViewModelBase, ILoadableViewModel
{
    [ObservableProperty]
    private string summary = "Keine Daten geladen.";

    public ObservableCollection<string> Highlights { get; } = [];

    [RelayCommand]
    public async Task LoadAsync()
    {
        IsBusy = true;
        try
        {
            var metrics = await libraryService.GetDashboardMetricsAsync();
            Summary = $"{metrics.BookCount} Buecher, {metrics.AuthorCount} Autoren, {metrics.PublisherCount} Verlage, {metrics.PlaceCount} Orte";
            Highlights.Clear();
            Highlights.Add($"Buecher pro Autor: {(metrics.AuthorCount == 0 ? 0 : (double)metrics.BookCount / metrics.AuthorCount):0.0}");
            Highlights.Add($"Buecher pro Verlag: {(metrics.PublisherCount == 0 ? 0 : (double)metrics.BookCount / metrics.PublisherCount):0.0}");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
