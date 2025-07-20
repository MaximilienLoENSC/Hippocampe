public class Compositeur
{
    public int Id { get; set; }
    public string Nom { get; set; } = null!;
    public string Prenom { get; set; } = null!;
    public List<Film> Films { get; set; } = new();

    // Default constructor
    public Compositeur() { }
}