public class Genre
{
    public int Id { get; set; }
    public string Nom { get; set; } = null!;
    public List<Film> Films { get; set; } = new();

    // Default constructor
    public Genre() { }
}