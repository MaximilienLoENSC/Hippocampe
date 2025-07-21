using System.Text.Json.Serialization;

public class Compositeur
{
    public int Id { get; set; }
    public string Nom { get; set; } = null!;
    public List<Film> Films { get; set; } = new();

    public Compositeur() { }

    public Compositeur(CompositeurDto compositeurDto)
    {
        if (compositeurDto == null)
            throw new ArgumentNullException(nameof(compositeurDto));

        Id = compositeurDto.Id;
        Nom = compositeurDto.Nom;
    }
}
