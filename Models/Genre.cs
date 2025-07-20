public class Genre
{
    public int Id { get; set; }
    public string Nom { get; set; } = null!;

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
