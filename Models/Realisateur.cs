public class Realisateur
{
    public int Id { get; set; }
    public string Nom { get; set; } = null!;
    public List<Film> Films { get; set; } = new();

    public Realisateur() { }

    public Realisateur(RealisateurDto realisateurDto)
    {
        if (realisateurDto == null)
            throw new ArgumentNullException(nameof(realisateurDto));

        Id = realisateurDto.Id;
        Nom = realisateurDto.Nom;
    }
}
