using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Bibliothek.ViewModels;

public sealed partial class AuthorEditorViewModel(Autor author) : ObservableValidator
{
    public Autor Author { get; } = author;

    [ObservableProperty]
    [Required(ErrorMessage = "Name ist erforderlich.")]
    private string name = author.Name;

    [ObservableProperty]
    [Range(0, 2100, ErrorMessage = "Jahrgang ist ungueltig.")]
    private int yearOfBirth = author.Jahrgang;

    public bool Commit()
    {
        ValidateAllProperties();
        if (HasErrors)
        {
            return false;
        }

        Author.Name = Name.Trim();
        Author.Jahrgang = YearOfBirth;
        return true;
    }
}
