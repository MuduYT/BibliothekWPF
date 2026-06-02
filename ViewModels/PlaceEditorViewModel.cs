using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Bibliothek.ViewModels;

public sealed partial class PlaceEditorViewModel(Ort place) : ObservableValidator
{
    public Ort Place { get; } = place;

    [ObservableProperty]
    [Required(ErrorMessage = "Name ist erforderlich.")]
    private string name = place.Name;

    [ObservableProperty]
    [Range(0, 99999, ErrorMessage = "Postleitzahl ist ungueltig.")]
    private int postalCode = place.Postleitzahl;

    public bool Commit()
    {
        ValidateAllProperties();
        if (HasErrors)
        {
            return false;
        }

        Place.Name = Name.Trim();
        Place.Postleitzahl = PostalCode;
        return true;
    }
}
