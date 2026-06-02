using CommunityToolkit.Mvvm.Input;
using Bibliothek.Services;

namespace Bibliothek.ViewModels;

public sealed partial class PlacesViewModel(
    ILibraryService libraryService,
    IDialogService dialogService) : ListViewModelBase<Ort>
{
    [RelayCommand]
    public override async Task LoadAsync()
        => await RunAsync(async () => ReplaceItems(await libraryService.GetPlacesAsync()));

    [RelayCommand]
    private async Task AddAsync()
    {
        var place = new Ort();
        if (dialogService.EditPlace(place))
        {
            await RunAsync(async () =>
            {
                await libraryService.SavePlaceAsync(place);
                ReplaceItems(await libraryService.GetPlacesAsync());
            });
        }
    }

    [RelayCommand]
    private async Task EditAsync()
    {
        if (SelectedItem is null)
        {
            return;
        }

        var place = new Ort { Id = SelectedItem.Id, Name = SelectedItem.Name, Postleitzahl = SelectedItem.Postleitzahl };
        if (dialogService.EditPlace(place))
        {
            await RunAsync(async () =>
            {
                await libraryService.SavePlaceAsync(place);
                ReplaceItems(await libraryService.GetPlacesAsync());
            });
        }
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (SelectedItem is null
            || !dialogService.Confirm("Ort loeschen", $"Soll '{SelectedItem.Name}' wirklich geloescht werden?"))
        {
            return;
        }

        await RunAsync(async () =>
        {
            await libraryService.DeletePlaceAsync(SelectedItem);
            ReplaceItems(await libraryService.GetPlacesAsync());
        });
    }

    protected override bool MatchesSearch(Ort item, string searchText)
        => string.IsNullOrWhiteSpace(searchText)
           || item.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase)
           || item.Postleitzahl.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase);
}
