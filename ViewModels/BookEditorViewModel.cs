using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Bibliothek.ViewModels;

public sealed partial class BookEditorViewModel : ObservableValidator
{
    public BookEditorViewModel(Buch book, IReadOnlyList<Autor> authors, IReadOnlyList<Verlag> publishers)
    {
        Book = book;
        Authors = new ObservableCollection<Autor>(authors);
        Publishers = new ObservableCollection<Verlag>(publishers);
        Title = book.Titel;
        Isbn = book.ISBN;
        PageCount = book.AnzahlSeiten;
        PublicationYear = book.Erscheinungsjahr;
        SelectedAuthor = book.Autor;
        SelectedPublisher = book.Verlag;
        IsIsbnEditable = string.IsNullOrWhiteSpace(book.ISBN);
    }

    public Buch Book { get; }
    public ObservableCollection<Autor> Authors { get; }
    public ObservableCollection<Verlag> Publishers { get; }
    public bool IsIsbnEditable { get; }

    [ObservableProperty]
    [Required(ErrorMessage = "Titel ist erforderlich.")]
    private string title = string.Empty;

    [ObservableProperty]
    [Required(ErrorMessage = "ISBN ist erforderlich.")]
    private string isbn = string.Empty;

    [ObservableProperty]
    [Range(0, 5000, ErrorMessage = "Seitenanzahl ist ungueltig.")]
    private int pageCount;

    [ObservableProperty]
    [Range(0, 2100, ErrorMessage = "Erscheinungsjahr ist ungueltig.")]
    private int publicationYear;

    [ObservableProperty]
    private Autor? selectedAuthor;

    [ObservableProperty]
    private Verlag? selectedPublisher;

    public bool Commit()
    {
        ValidateAllProperties();
        if (HasErrors)
        {
            return false;
        }

        Book.Titel = Title.Trim();
        Book.ISBN = Isbn.Trim();
        Book.AnzahlSeiten = PageCount;
        Book.Erscheinungsjahr = PublicationYear;
        Book.Autor = SelectedAuthor;
        Book.Verlag = SelectedPublisher;
        return true;
    }
}
