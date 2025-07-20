public class Film
{
    public int Id { get; set; }
    public string Titre { get; set; } = null!;
    public DateTime DateDeSortie { get; set; }
    public List<Genre> Genres { get; set; } = new();
    public List<Pays> Pays { get; set; } = new();
    public List<Realisateur> Realisateurs { get; set; } = new();
    public List<Acteur> Acteurs { get; set; } = new();
    public List<Compositeur> Compositeurs { get; set; } = new();
    public string Commentaire { get; set; } = null!;

    // Default constructor
    public Film() { }
}