public class Genre : INommable
{
    public int Id { get; set; }
    public string Nom { get; set; } = null!;
    public List<Film> Films { get; set; } = new();

    public Genre() { }

    public Genre(GenreDto genreDto)
    {
        if (genreDto == null)
            throw new ArgumentNullException(nameof(genreDto));

        Id = genreDto.Id;
        Nom = genreDto.Nom;
    }

    public List<Film> GetFilms(List<Film> films)
    {
        return films.Where(f => f.Genres.Any(a => a.Id == Id)).ToList();
    }
}
