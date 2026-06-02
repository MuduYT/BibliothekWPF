using System.ComponentModel.DataAnnotations;

namespace Bibliothek;

public class Autor
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Der Name ist erforderlich.")]
    [StringLength(120, ErrorMessage = "Der Name darf maximal 120 Zeichen lang sein.")]
    public string Name { get; set; } = string.Empty;

    [Range(0, 2100, ErrorMessage = "Der Jahrgang muss plausibel sein.")]
    public int Jahrgang { get; set; }

    public List<Buch> Buecher { get; set; } = new();
}
