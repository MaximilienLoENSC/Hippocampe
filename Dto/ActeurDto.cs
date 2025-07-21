using System.Text.Json.Serialization;

public class ActeurDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("nom")]
    public string Nom { get; set; } = null!;

    [JsonPropertyName("films")]
    public List<string> Films { get; set; } = new();

    public ActeurDto() { }

    public ActeurDto(Acteur acteur)
    {
        Id = acteur.Id;
        Nom = acteur.Nom;
        Films = acteur.Films.Select(f => f.Titre).ToList();
    }
}
