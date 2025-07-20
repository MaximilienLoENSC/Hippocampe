using System.Text.Json.Serialization;

public class ActeurDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("nom")]
    public string Nom { get; set; } = null!;

    [JsonPropertyName("prenom")]
    public string Prenom { get; set; } = null!;

    public ActeurDto() { }

    public ActeurDto(Acteur acteur)
    {
        Id = acteur.Id;
        Nom = acteur.Nom;
        Prenom = acteur.Prenom;
    }
}
