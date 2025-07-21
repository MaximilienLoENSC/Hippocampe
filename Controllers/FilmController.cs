using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/film")]
public class FilmController : ControllerBase
{
    private readonly DataContext _context;

    public FilmController(DataContext context)
    {
        _context = context;
    }

    // GET: api/film
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FilmDto>>> GetFilms()
    {
        var filmsDto = await _context
            .Films.Include(f => f.Genres)
            .Include(f => f.Pays)
            .Include(f => f.Realisateurs)
            .Include(f => f.Acteurs)
            .Include(f => f.Compositeurs)
            .Select(f => new FilmDto(f))
            .ToListAsync();

        return filmsDto;
    }

    // GET: api/film/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<FilmDto>> GetFilm(int id)
    {
        var film = await _context
            .Films.Include(f => f.Genres)
            .Include(f => f.Pays)
            .Include(f => f.Realisateurs)
            .Include(f => f.Acteurs)
            .Include(f => f.Compositeurs)
            .SingleOrDefaultAsync(f => f.Id == id);

        if (film == null)
            return NotFound($"Aucun film trouvé avec l'ID {id}.");

        return new FilmDto(film);
    }

    // POST: api/film
    [HttpPost]
    public async Task<ActionResult<Film>> PostFilm([FromBody] FilmDto filmDto)
    {
        if (filmDto.DateDeSortie == DateTime.MinValue)
            return BadRequest("Date de sortie invalide.");

        var genres = await GetOrCreateEntitiesAsync<Genre>(filmDto.Genres);
        var pays = await GetOrCreateEntitiesAsync<Pays>(filmDto.Pays);
        var realisateurs = await GetOrCreateEntitiesAsync<Realisateur>(filmDto.Realisateurs);
        var acteurs = await GetOrCreateEntitiesAsync<Acteur>(filmDto.Acteurs);
        var compositeurs = await GetOrCreateEntitiesAsync<Compositeur>(filmDto.Compositeurs);

        var film = new Film
        {
            Titre = filmDto.Titre,
            DateDeSortie = filmDto.DateDeSortie,
            Commentaire = filmDto.Commentaire,
            Genres = genres,
            Pays = pays,
            Realisateurs = realisateurs,
            Acteurs = acteurs,
            Compositeurs = compositeurs,
        };

        _context.Films.Add(film);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFilm), new { id = film.Id }, new FilmDto(film));
    }

    // PUT: api/film/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutFilm(int id, [FromBody] FilmDto filmDto)
    {
        if (id != filmDto.Id)
            return BadRequest("L'ID dans l'URL ne correspond pas à celui du film.");

        var film = await _context
            .Films.Include(f => f.Genres)
            .Include(f => f.Pays)
            .Include(f => f.Realisateurs)
            .Include(f => f.Acteurs)
            .Include(f => f.Compositeurs)
            .FirstOrDefaultAsync(f => f.Id == id);

        if (film == null)
            return NotFound($"Film ID {id} introuvable.");

        film.Titre = filmDto.Titre;
        film.DateDeSortie = filmDto.DateDeSortie;
        film.Commentaire = filmDto.Commentaire;
        film.Genres = await GetOrCreateEntitiesAsync<Genre>(filmDto.Genres);
        film.Pays = await GetOrCreateEntitiesAsync<Pays>(filmDto.Pays);
        film.Realisateurs = await GetOrCreateEntitiesAsync<Realisateur>(filmDto.Realisateurs);
        film.Acteurs = await GetOrCreateEntitiesAsync<Acteur>(filmDto.Acteurs);
        film.Compositeurs = await GetOrCreateEntitiesAsync<Compositeur>(filmDto.Compositeurs);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/film/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFilm(int id)
    {
        var film = await _context.Films.FindAsync(id);

        if (film == null)
            return NotFound($"Aucun film trouvé avec l'id {id}.");

        _context.Films.Remove(film);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    //Utilisation pour de la comparaison d'éléments e classes filles de INommable avec ceux présnets dans la BDD
    private async Task<List<T>> GetOrCreateEntitiesAsync<T>(List<string> noms)
        where T : class, INommable, new()
    {
        var results = new List<T>();
        foreach (var nom in noms.Distinct())
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(e => e.Nom == nom);
            if (entity == null)
            {
                entity = new T { Nom = nom };
                _context.Set<T>().Add(entity);
            }
            results.Add(entity);
        }
        return results;
    }
}
