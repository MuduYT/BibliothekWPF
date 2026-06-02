using System.ComponentModel.DataAnnotations;

namespace Bibliothek;

public class Verlag
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Der Name ist erforderlich.")]
    [StringLength(140, ErrorMessage = "Der Name darf maximal 140 Zeichen lang sein.")]
    public string Name { get; set; } = string.Empty;

    public Ort? Firmensitz { get; set; }

    [StringLength(40, ErrorMessage = "Die Telefonnummer darf maximal 40 Zeichen lang sein.")]
    public string Telefonnummer { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "Die E-Mail-Adresse ist ungueltig.")]
    [StringLength(160, ErrorMessage = "Die E-Mail-Adresse darf maximal 160 Zeichen lang sein.")]
    public string Email { get; set; } = string.Empty;

    public List<Buch> Buecher { get; set; } = new();
}
