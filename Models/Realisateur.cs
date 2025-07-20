public class Realisateur
{
    public int Id { get; set; }
    public string Nom { get; set; } = null!;
    public string Prenom { get; set; } = null!;

    public Realisateur() { }

    public Realisateur(RealisateurDto realisateurDto)
    {
        if (realisateurDto == null)
            throw new ArgumentNullException(nameof(realisateurDto));

        Id = realisateurDto.Id;
        Nom = realisateurDto.Nom;
        Prenom = realisateurDto.Prenom;
    }

    public List<Film> GetFilms(List<Film> films)
    {
        return films.Where(f => f.Realisateurs.Any(a => a.Id == Id)).ToList();
    }
}
