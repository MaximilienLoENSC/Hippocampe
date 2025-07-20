using System.Text.Json.Serialization;

public class PaysDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("nom")]
    public string Nom { get; set; } = null!;

    public PaysDto() { }

    public PaysDto(Pays pays)
    {
        Id = pays.Id;
        Nom = pays.Nom;
    }
}
