using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Film
{
    public int Id { get; set; }
    public string Titre { get; set; } = null!;
    public DateTime DateDeSortie { get; set; }
    public List<Genre> Genres { get; set; } = new();
    public List<Pays> Pays { get; set; } = new();
    public List<Realisateur> Realisateurs { get; set; } = new();
    public List<Acteur> Acteurs { get; set; } = new();
    public List<Compositeur> Compositeurs { get; set; } = new();
    public string Commentaire { get; set; } = null!;

    public Film() { }

    public Film(
        FilmDto filmDto,
        List<Genre> genres,
        List<Pays> pays,
        List<Realisateur> realisateurs,
        List<Acteur> acteurs,
        List<Compositeur> compositeurs
    )
    {
        if (filmDto == null)
            throw new ArgumentNullException(nameof(filmDto));

        Id = filmDto.Id;
        Titre = filmDto.Titre;
        DateDeSortie = filmDto.DateDeSortie;
        Commentaire = filmDto.Commentaire;

        Genres = genres.Where(g => filmDto.GenreIds.Contains(g.Id)).ToList();
        Pays = pays.Where(p => filmDto.PaysIds.Contains(p.Id)).ToList();
        Realisateurs = realisateurs.Where(r => filmDto.RealisateurIds.Contains(r.Id)).ToList();
        Acteurs = acteurs.Where(a => filmDto.ActeurIds.Contains(a.Id)).ToList();
        Compositeurs = compositeurs.Where(c => filmDto.CompositeurIds.Contains(c.Id)).ToList();
    }
}

//Classe de conversion DateTime vers string “YYYY-MM-DD"
public class MyCustomJsonConverter : JsonConverter<DateTime>
{
    public override DateTime Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var dateTimeFromJson = reader.GetString()!;
        if (Format.UnauthorizedDateFormat(dateTimeFromJson))
        {
            dateTimeFromJson = "0001-01-01";
        }
        return DateTime.ParseExact(dateTimeFromJson, "yyyy-MM-dd", CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        string formattedDate = value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        writer.WriteStringValue(formattedDate);
    }
}

public static class Format
{
    public static bool UnauthorizedDateFormat(DateTime value)
    {
        // Convertir le DateTime en chaîne dans le format "yyyy-MM-dd"
        string dateString = value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

        // Vérifier si la chaîne respecte le format "yyyy-MM-dd"
        if (
            !DateTime.TryParseExact(
                dateString,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _
            )
        )
        {
            return true; // Format incorrect
        }

        return false; // Format correct
    }

    public static bool UnauthorizedDateFormat(string date)
    {
        if (
            !DateTime.TryParseExact(
                date,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _
            )
        )
            return true;
        return false;
    }
}
