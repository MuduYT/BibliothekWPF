using CommunityToolkit.Mvvm.Input;
using Bibliothek.Services;

namespace Bibliothek.ViewModels;

public sealed partial class AuthorsViewModel(
    ILibraryService libraryService,
    IDialogService dialogService) : ListViewModelBase<Autor>
{
    [RelayCommand]
    public override async Task LoadAsync()
        => await RunAsync(async () => ReplaceItems(await libraryService.GetAuthorsAsync()));

    [RelayCommand]
    private async Task AddAsync()
    {
        var author = new Autor();
        if (dialogService.EditAuthor(author))
        {
            await RunAsync(async () =>
            {
                await libraryService.SaveAuthorAsync(author);
                ReplaceItems(await libraryService.GetAuthorsAsync());
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

        var author = new Autor { Id = SelectedItem.Id, Name = SelectedItem.Name, Jahrgang = SelectedItem.Jahrgang };
        if (dialogService.EditAuthor(author))
        {
            await RunAsync(async () =>
            {
                await libraryService.SaveAuthorAsync(author);
                ReplaceItems(await libraryService.GetAuthorsAsync());
            });
        }
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (SelectedItem is null
            || !dialogService.Confirm("Autor loeschen", $"Soll '{SelectedItem.Name}' wirklich geloescht werden?"))
        {
            return;
        }

        await RunAsync(async () =>
        {
            await libraryService.DeleteAuthorAsync(SelectedItem);
            ReplaceItems(await libraryService.GetAuthorsAsync());
        });
    }

    protected override bool MatchesSearch(Autor item, string searchText)
        => string.IsNullOrWhiteSpace(searchText)
           || item.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase)
           || item.Jahrgang.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase);
}
