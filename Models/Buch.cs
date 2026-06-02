using System.ComponentModel.DataAnnotations;

namespace Bibliothek;

public class Buch
{
    [Required(ErrorMessage = "Der Titel ist erforderlich.")]
    [StringLength(180, ErrorMessage = "Der Titel darf maximal 180 Zeichen lang sein.")]
    public string Titel { get; set; } = string.Empty;

    [Key]
    [Required(ErrorMessage = "Die ISBN ist erforderlich.")]
    [StringLength(32, ErrorMessage = "Die ISBN darf maximal 32 Zeichen lang sein.")]
    public string ISBN { get; set; } = string.Empty;

    [Range(0, 5000, ErrorMessage = "Die Seitenanzahl muss plausibel sein.")]
    public int AnzahlSeiten { get; set; }

    [Range(0, 2100, ErrorMessage = "Das Erscheinungsjahr muss plausibel sein.")]
    public int Erscheinungsjahr { get; set; }

    public Verlag? Verlag { get; set; }

    public Autor? Autor { get; set; }
}
