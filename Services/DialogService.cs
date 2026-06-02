using System.Windows;
using Bibliothek.ViewModels;
using Bibliothek.Views;

namespace Bibliothek.Services;

public sealed class DialogService : IDialogService
{
    public bool Confirm(string title, string message)
        => MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes;

    public void ShowError(string title, string message)
        => MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);

    public bool EditBook(Buch book, IReadOnlyList<Autor> authors, IReadOnlyList<Verlag> publishers)
    {
        var viewModel = new BookEditorViewModel(book, authors, publishers);
        return ShowDialog(new BookDialogView { DataContext = viewModel }, viewModel.Commit);
    }

    public bool EditAuthor(Autor author)
    {
        var viewModel = new AuthorEditorViewModel(author);
        return ShowDialog(new AuthorDialogView { DataContext = viewModel }, viewModel.Commit);
    }

    public bool EditPublisher(Verlag publisher, IReadOnlyList<Ort> places)
    {
        var viewModel = new PublisherEditorViewModel(publisher, places);
        return ShowDialog(new PublisherDialogView { DataContext = viewModel }, viewModel.Commit);
    }

    public bool EditPlace(Ort place)
    {
        var viewModel = new PlaceEditorViewModel(place);
        return ShowDialog(new PlaceDialogView { DataContext = viewModel }, viewModel.Commit);
    }

    private static bool ShowDialog(Window dialog, Func<bool> commit)
    {
        dialog.Owner = Application.Current.MainWindow;
        dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        dialog.Tag = commit;
        return dialog.ShowDialog() == true;
    }
}
