using CommunityToolkit.Mvvm.Input;
using Bibliothek.Services;

namespace Bibliothek.ViewModels;

public sealed partial class BooksViewModel(
    ILibraryService libraryService,
    IDialogService dialogService) : ListViewModelBase<Buch>
{
    [RelayCommand]
    public override async Task LoadAsync()
        => await RunAsync(async () => ReplaceItems(await libraryService.GetBooksAsync()));

    [RelayCommand]
    private async Task AddAsync()
    {
        var authors = await libraryService.GetAuthorsAsync();
        var publishers = await libraryService.GetPublishersAsync();
        var book = new Buch { Erscheinungsjahr = DateTime.Now.Year };

        if (dialogService.EditBook(book, authors, publishers))
        {
            await RunAsync(async () =>
            {
                await libraryService.SaveBookAsync(book);
                ReplaceItems(await libraryService.GetBooksAsync());
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

        var authors = await libraryService.GetAuthorsAsync();
        var publishers = await libraryService.GetPublishersAsync();
        var book = new Buch
        {
            ISBN = SelectedItem.ISBN,
            Titel = SelectedItem.Titel,
            AnzahlSeiten = SelectedItem.AnzahlSeiten,
            Erscheinungsjahr = SelectedItem.Erscheinungsjahr,
            Autor = authors.FirstOrDefault(a => a.Id == SelectedItem.Autor?.Id),
            Verlag = publishers.FirstOrDefault(v => v.Id == SelectedItem.Verlag?.Id)
        };

        if (dialogService.EditBook(book, authors, publishers))
        {
            await RunAsync(async () =>
            {
                await libraryService.SaveBookAsync(book);
                ReplaceItems(await libraryService.GetBooksAsync());
            });
        }
    }

    [RelayCommand]
    private void Details()
    {
        if (SelectedItem is null)
        {
            return;
        }

        dialogService.ShowError("Buchdetails",
            $"{SelectedItem.Titel}\nISBN: {SelectedItem.ISBN}\nAutor: {SelectedItem.Autor?.Name ?? "-"}\nVerlag: {SelectedItem.Verlag?.Name ?? "-"}");
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (SelectedItem is null
            || !dialogService.Confirm("Buch loeschen", $"Soll '{SelectedItem.Titel}' wirklich geloescht werden?"))
        {
            return;
        }

        await RunAsync(async () =>
        {
            await libraryService.DeleteBookAsync(SelectedItem);
            ReplaceItems(await libraryService.GetBooksAsync());
        });
    }

    protected override bool MatchesSearch(Buch item, string searchText)
        => string.IsNullOrWhiteSpace(searchText)
           || item.Titel.Contains(searchText, StringComparison.OrdinalIgnoreCase)
           || item.ISBN.Contains(searchText, StringComparison.OrdinalIgnoreCase)
           || (item.Autor?.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ?? false)
           || (item.Verlag?.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ?? false);
}
