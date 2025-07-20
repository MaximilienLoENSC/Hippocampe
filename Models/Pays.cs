public class Pays
{
    public int Id { get; set; }
    public string Nom { get; set; } = null!;
    public List<Film> Films { get; set; } = new();

    // Default constructor
    public Pays() { }
}