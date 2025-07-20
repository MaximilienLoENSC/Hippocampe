using System.Text.Json.Serialization;

public class Compositeur
{
    public int Id { get; set; }
    public string Nom { get; set; } = null!;
    public string Prenom { get; set; } = null!;

    public Compositeur() { }

    public Compositeur(CompositeurDto compositeurDto)
    {
        if (compositeurDto == null)
            throw new ArgumentNullException(nameof(compositeurDto));

        Id = compositeurDto.Id;
        Nom = compositeurDto.Nom;
        Prenom = compositeurDto.Prenom;
    }

    public List<Film> GetFilms(List<Film> films)
    {
        return films.Where(f => f.Compositeurs.Any(a => a.Id == Id)).ToList();
    }
}
