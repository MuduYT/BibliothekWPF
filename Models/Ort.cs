using System.ComponentModel.DataAnnotations;

namespace Bibliothek;

public class Ort
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Der Ortsname ist erforderlich.")]
    [StringLength(120, ErrorMessage = "Der Ortsname darf maximal 120 Zeichen lang sein.")]
    public string Name { get; set; } = string.Empty;

    [Range(0, 99999, ErrorMessage = "Die Postleitzahl muss plausibel sein.")]
    public int Postleitzahl { get; set; }

    public List<Verlag> Verlage { get; set; } = new();
}
