namespace Bibliothek.Services;

public interface IDialogService
{
    bool Confirm(string title, string message);
    void ShowError(string title, string message);
    bool EditBook(Buch book, IReadOnlyList<Autor> authors, IReadOnlyList<Verlag> publishers);
    bool EditAuthor(Autor author);
    bool EditPublisher(Verlag publisher, IReadOnlyList<Ort> places);
    bool EditPlace(Ort place);
}
