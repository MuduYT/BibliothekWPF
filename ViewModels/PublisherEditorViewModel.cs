using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Bibliothek.ViewModels;

public sealed partial class PublisherEditorViewModel : ObservableValidator
{
    public PublisherEditorViewModel(Verlag publisher, IReadOnlyList<Ort> places)
    {
        Publisher = publisher;
        Places = new ObservableCollection<Ort>(places);
        Name = publisher.Name;
        Phone = publisher.Telefonnummer;
        Email = publisher.Email;
        SelectedPlace = publisher.Firmensitz;
    }

    public Verlag Publisher { get; }
    public ObservableCollection<Ort> Places { get; }

    [ObservableProperty]
    [Required(ErrorMessage = "Name ist erforderlich.")]
    private string name = string.Empty;

    [ObservableProperty]
    private string phone = string.Empty;

    [ObservableProperty]
    [EmailAddress(ErrorMessage = "E-Mail ist ungueltig.")]
    private string email = string.Empty;

    [ObservableProperty]
    private Ort? selectedPlace;

    public bool Commit()
    {
        ValidateAllProperties();
        if (HasErrors)
        {
            return false;
        }

        Publisher.Name = Name.Trim();
        Publisher.Telefonnummer = Phone.Trim();
        Publisher.Email = Email.Trim();
        Publisher.Firmensitz = SelectedPlace;
        return true;
    }
}
