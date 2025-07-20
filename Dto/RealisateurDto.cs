using System.Text.Json.Serialization;

public class RealisateurDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("nom")]
    public string Nom { get; set; } = null!;

    [JsonPropertyName("prenom")]
    public string Prenom { get; set; } = null!;

    public RealisateurDto() { }

    public RealisateurDto(Realisateur realisateur)
    {
        Id = realisateur.Id;
        Nom = realisateur.Nom;
        Prenom = realisateur.Prenom;
    }
}
