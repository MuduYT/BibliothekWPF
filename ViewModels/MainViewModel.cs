using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Bibliothek.ViewModels;

public sealed partial class MainViewModel : ViewModelBase
{
    public MainViewModel(
        DashboardViewModel dashboard,
        BooksViewModel books,
        AuthorsViewModel authors,
        PublishersViewModel publishers,
        PlacesViewModel places,
        StatisticsViewModel statistics,
        SettingsViewModel settings)
    {
        var startseite = new NavigationItemViewModel("Startseite", "Home", dashboard);
        var orte = new NavigationItemViewModel("Orte", "Places", places);
        var verlage = new NavigationItemViewModel("Verlage", "Publishers", publishers);
        var authorsItem = new NavigationItemViewModel("Authors", "Authors", authors);
        var booksItem = new NavigationItemViewModel("Books", "Books", books);
        var settingsItem = new NavigationItemViewModel("Einstellungen", "Settings", settings);

        NavigationItems = [startseite, orte, verlage, authorsItem, booksItem, settingsItem];
        MainNavigationItems = [startseite, orte, verlage, authorsItem, booksItem];
        TableNavigationItems = [orte, verlage, authorsItem, booksItem];
        SettingsNavigationItem = settingsItem;

        SelectNavigationItem(startseite);
    }

    public ObservableCollection<NavigationItemViewModel> NavigationItems { get; }
    public ObservableCollection<NavigationItemViewModel> MainNavigationItems { get; }
    public ObservableCollection<NavigationItemViewModel> TableNavigationItems { get; }
    public NavigationItemViewModel SettingsNavigationItem { get; }

    [ObservableProperty]
    private ViewModelBase? currentViewModel;

    public bool ShowBackButton => CurrentViewModel is not DashboardViewModel;

    partial void OnCurrentViewModelChanged(ViewModelBase? value)
    {
        OnPropertyChanged(nameof(ShowBackButton));
    }

    [RelayCommand]
    private void GoHome()
    {
        SelectNavigationItem(NavigationItems[0]);
    }

    [RelayCommand]
    private async Task NavigateAsync(NavigationItemViewModel item)
    {
        SelectNavigationItem(item);

        if (item.ViewModel is ILoadableViewModel loadable)
        {
            await loadable.LoadAsync();
        }
    }

    private void SelectNavigationItem(NavigationItemViewModel item)
    {
        foreach (var navigationItem in NavigationItems)
        {
            navigationItem.IsSelected = navigationItem == item;
        }

        CurrentViewModel = item.ViewModel;
    }
}
