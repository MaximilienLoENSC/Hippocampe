using System.Text.Json.Serialization;

public class FilmOutputDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("titre")]
    public string Titre { get; set; } = null!;

    [JsonPropertyName("date_de_sortie")]
    [JsonConverter(typeof(MyCustomJsonConverter))]
    public DateTime DateDeSortie { get; set; }

    [JsonPropertyName("genres")]
    public List<string> Genres { get; set; } = new();

    [JsonPropertyName("pays")]
    public List<string> Pays { get; set; } = new();

    [JsonPropertyName("realisateurs")]
    public List<string> Realisateurs { get; set; } = new();

    [JsonPropertyName("acteurs")]
    public List<string> Acteurs { get; set; } = new();

    [JsonPropertyName("compositeurs")]
    public List<string> Compositeurs { get; set; } = new();

    [JsonPropertyName("commentaire")]
    public string Commentaire { get; set; } = null!;

    public FilmOutputDto() { }

    public FilmOutputDto(Film film)
    {
        Id = film.Id;
        Titre = film.Titre;
        DateDeSortie = film.DateDeSortie;
        Commentaire = film.Commentaire;

        Genres = film.Genres.Select(g => g.Nom).ToList();
        Pays = film.Pays.Select(p => p.Nom).ToList();
        Realisateurs = film.Realisateurs.Select(r => r.Nom).ToList();
        Acteurs = film.Acteurs.Select(a => a.Nom).ToList();
        Compositeurs = film.Compositeurs.Select(c => c.Nom).ToList();
    }
}
