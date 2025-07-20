using System.Text.Json.Serialization;

public class FilmDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("titre")]
    public string Titre { get; set; } = null!;

    [JsonPropertyName("date_de_sortie")]
    [JsonConverter(typeof(MyCustomJsonConverter))]
    public DateTime DateDeSortie { get; set; }

    [JsonPropertyName("genre_ids")]
    public List<int> GenreIds { get; set; } = new();

    [JsonPropertyName("pays_ids")]
    public List<int> PaysIds { get; set; } = new();

    [JsonPropertyName("realisateur_ids")]
    public List<int> RealisateurIds { get; set; } = new();

    [JsonPropertyName("acteur_ids")]
    public List<int> ActeurIds { get; set; } = new();

    [JsonPropertyName("compositeur_ids")]
    public List<int> CompositeurIds { get; set; } = new();

    [JsonPropertyName("commentaire")]
    public string Commentaire { get; set; } = null!;

    public FilmDto() { }

    public FilmDto(Film film)
    {
        Id = film.Id;
        Titre = film.Titre;
        DateDeSortie = film.DateDeSortie;
        Commentaire = film.Commentaire;

        GenreIds = film.Genres.Select(g => g.Id).ToList();
        PaysIds = film.Pays.Select(p => p.Id).ToList();
        RealisateurIds = film.Realisateurs.Select(r => r.Id).ToList();
        ActeurIds = film.Acteurs.Select(a => a.Id).ToList();
        CompositeurIds = film.Compositeurs.Select(c => c.Id).ToList();
    }
}
