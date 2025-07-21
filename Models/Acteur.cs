public class Acteur
{
    public int Id { get; set; }
    public string Nom { get; set; } = null!;
    public List<Film> Films { get; set; } = new();

    public Acteur() { }

    public Acteur(ActeurDto acteurDto)
    {
        if (acteurDto == null)
            throw new ArgumentNullException(nameof(acteurDto));

        Id = acteurDto.Id;
        Nom = acteurDto.Nom;
    }
}
