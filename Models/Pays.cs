public class Pays
{
    public int Id { get; set; }
    public string Nom { get; set; } = null!;

    public Pays() { }

    public Pays(PaysDto paysDto)
    {
        if (paysDto == null)
            throw new ArgumentNullException(nameof(paysDto));

        Id = paysDto.Id;
        Nom = paysDto.Nom;
    }

    public List<Film> GetFilms(List<Film> films)
    {
        return films.Where(f => f.Pays.Any(a => a.Id == Id)).ToList();
    }
}
