using CommunityToolkit.Mvvm.Input;
using Bibliothek.Services;

namespace Bibliothek.ViewModels;

public sealed partial class PublishersViewModel(
    ILibraryService libraryService,
    IDialogService dialogService) : ListViewModelBase<Verlag>
{
    [RelayCommand]
    public override async Task LoadAsync()
        => await RunAsync(async () => ReplaceItems(await libraryService.GetPublishersAsync()));

    [RelayCommand]
    private async Task AddAsync()
    {
        var places = await libraryService.GetPlacesAsync();
        var publisher = new Verlag();
        if (dialogService.EditPublisher(publisher, places))
        {
            await RunAsync(async () =>
            {
                await libraryService.SavePublisherAsync(publisher);
                ReplaceItems(await libraryService.GetPublishersAsync());
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

        var places = await libraryService.GetPlacesAsync();
        var publisher = new Verlag
        {
            Id = SelectedItem.Id,
            Name = SelectedItem.Name,
            Telefonnummer = SelectedItem.Telefonnummer,
            Email = SelectedItem.Email,
            Firmensitz = places.FirstOrDefault(o => o.Id == SelectedItem.Firmensitz?.Id)
        };

        if (dialogService.EditPublisher(publisher, places))
        {
            await RunAsync(async () =>
            {
                await libraryService.SavePublisherAsync(publisher);
                ReplaceItems(await libraryService.GetPublishersAsync());
            });
        }
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (SelectedItem is null
            || !dialogService.Confirm("Verlag loeschen", $"Soll '{SelectedItem.Name}' wirklich geloescht werden?"))
        {
            return;
        }

        await RunAsync(async () =>
        {
            await libraryService.DeletePublisherAsync(SelectedItem);
            ReplaceItems(await libraryService.GetPublishersAsync());
        });
    }

    protected override bool MatchesSearch(Verlag item, string searchText)
        => string.IsNullOrWhiteSpace(searchText)
           || item.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase)
           || item.Email.Contains(searchText, StringComparison.OrdinalIgnoreCase)
           || (item.Firmensitz?.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ?? false);
}
