using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Bibliothek.Services;

namespace Bibliothek.ViewModels;

public sealed partial class DashboardViewModel(ILibraryService libraryService) : ViewModelBase, ILoadableViewModel
{
    [ObservableProperty]
    private int bookCount;

    [ObservableProperty]
    private int authorCount;

    [ObservableProperty]
    private int publisherCount;

    [ObservableProperty]
    private int placeCount;

    [ObservableProperty]
    private string quickSearch = string.Empty;

    public ObservableCollection<Buch> RecentBooks { get; } = [];

    [RelayCommand]
    public async Task LoadAsync()
    {
        IsBusy = true;
        ErrorMessage = null;
        try
        {
            var metrics = await libraryService.GetDashboardMetricsAsync();
            BookCount = metrics.BookCount;
            AuthorCount = metrics.AuthorCount;
            PublisherCount = metrics.PublisherCount;
            PlaceCount = metrics.PlaceCount;
            RecentBooks.Clear();
            foreach (var book in metrics.RecentBooks)
            {
                RecentBooks.Add(book);
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            IsBusy = false;
        }
    }
}
