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
    public async Task<ActionResult<IEnumerable<FilmOutputDto>>> GetFilms()
    {
        var filmsDto = await _context
            .Films.Include(f => f.Genres)
            .Include(f => f.Pays)
            .Include(f => f.Realisateurs)
            .Include(f => f.Acteurs)
            .Include(f => f.Compositeurs)
            .Select(f => new FilmOutputDto(f))
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
            return BadRequest("La date renseignée ne correspond pas au format attendu YYYY-MM-DD.");

        var genres = await _context
            .Genres.Where(g => filmDto.GenreIds.Contains(g.Id))
            .ToListAsync();
        var pays = await _context.Pays.Where(p => filmDto.PaysIds.Contains(p.Id)).ToListAsync();
        var realisateurs = await _context
            .Realisateurs.Where(r => filmDto.RealisateurIds.Contains(r.Id))
            .ToListAsync();
        var acteurs = await _context
            .Acteurs.Where(a => filmDto.ActeurIds.Contains(a.Id))
            .ToListAsync();
        var compositeurs = await _context
            .Compositeurs.Where(c => filmDto.CompositeurIds.Contains(c.Id))
            .ToListAsync();

        var film = new Film(filmDto, genres, pays, realisateurs, acteurs, compositeurs);

        _context.Films.Add(film);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFilm), new { id = film.Id }, film);
    }

    // PUT: api/film/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutFilm(int id, [FromBody] FilmDto filmDto)
    {
        if (id != filmDto.Id)
            return BadRequest(
                "L'id du film dans l'URL ne correspond pas à celui du corps de la requête."
            );

        if (filmDto.DateDeSortie == DateTime.MinValue)
            return BadRequest("La date renseignée ne correspond pas au format attendu YYYY-MM-DD.");

        var genres = await _context
            .Genres.Where(g => filmDto.GenreIds.Contains(g.Id))
            .ToListAsync();
        var pays = await _context.Pays.Where(p => filmDto.PaysIds.Contains(p.Id)).ToListAsync();
        var realisateurs = await _context
            .Realisateurs.Where(r => filmDto.RealisateurIds.Contains(r.Id))
            .ToListAsync();
        var acteurs = await _context
            .Acteurs.Where(a => filmDto.ActeurIds.Contains(a.Id))
            .ToListAsync();
        var compositeurs = await _context
            .Compositeurs.Where(c => filmDto.CompositeurIds.Contains(c.Id))
            .ToListAsync();

        var film = new Film(filmDto, genres, pays, realisateurs, acteurs, compositeurs);

        _context.Entry(film).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Films.Any(f => f.Id == id))
                return NotFound($"Aucun film trouvé avec l'id {id}.");
            else
                throw;
        }

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
}
