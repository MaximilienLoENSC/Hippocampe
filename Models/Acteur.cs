public class Acteur
{
    public int Id { get; set; }
    public string Nom { get; set; } = null!;
    public string Prenom { get; set; } = null!;

    public Acteur() { }

    public Acteur(ActeurDto acteurDto)
    {
        if (acteurDto == null)
            throw new ArgumentNullException(nameof(acteurDto));

        Id = acteurDto.Id;
        Nom = acteurDto.Nom;
        Prenom = acteurDto.Prenom;
    }

    public List<Film> GetFilms(List<Film> films)
    {
        return films.Where(f => f.Acteurs.Any(a => a.Id == Id)).ToList();
    }
}
