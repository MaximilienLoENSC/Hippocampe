using System.Text.Json.Serialization;

public class GenreDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("nom")]
    public string Nom { get; set; } = null!;

    [JsonPropertyName("films")]
    public List<string> Films { get; set; } = new();

    public GenreDto() { }

    public GenreDto(Genre genre)
    {
        Id = genre.Id;
        Nom = genre.Nom;
        Films = genre.Films.Select(f => f.Titre).ToList();
    }
}
