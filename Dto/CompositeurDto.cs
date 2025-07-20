using System.Text.Json.Serialization;

public class CompositeurDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("nom")]
    public string Nom { get; set; } = null!;

    [JsonPropertyName("prenom")]
    public string Prenom { get; set; } = null!;

    public CompositeurDto() { }

    public CompositeurDto(Compositeur compositeur)
    {
        Id = compositeur.Id;
        Nom = compositeur.Nom;
        Prenom = compositeur.Prenom;
    }
}
