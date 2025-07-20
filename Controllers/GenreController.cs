using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/genre")]
public class GenreController : ControllerBase
{
    private readonly DataContext _context;

    public GenreController(DataContext context)
    {
        _context = context;
    }

    // GET: api/genre
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GenreDto>>> GetGenres()
    {
        var genres = await _context.Genres.Include(a => a.Films).ToListAsync();

        var genresDto = genres.Select(a => new GenreDto(a)).ToList();

        return Ok(genresDto);
    }

    // GET: api/genre/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<GenreDto>> GetGenre(int id)
    {
        var genre = await _context
            .Genres.Include(a => a.Films)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (genre == null)
            return NotFound($"Aucun genre trouvé avec l'ID {id}.");

        return new GenreDto(genre);
    }

    // GET: api/genre/{id}/films
    [HttpGet("{id}/films")]
    public async Task<ActionResult<IEnumerable<FilmOutputDto>>> GetFilmsDeGenre(int id)
    {
        var genre = await _context
            .Genres.Include(a => a.Films)
            .ThenInclude(f => f.Genres)
            .Include(a => a.Films)
            .ThenInclude(f => f.Pays)
            .Include(a => a.Films)
            .ThenInclude(f => f.Realisateurs)
            .Include(a => a.Films)
            .ThenInclude(f => f.Compositeurs)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (genre == null)
            return NotFound($"Aucun genre trouvé avec l'ID {id}.");

        var filmsDeGenre = genre.Films.Select(f => new FilmOutputDto(f)).ToList();

        return Ok(filmsDeGenre);
    }

    /*
    // POST: api/genre
    [HttpPost]
    public async Task<ActionResult<Genre>> PostGenre([FromBody] GenreDto dto)
    {
        var genre = new Genre(dto);
        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetGenre), new { id = genre.Id }, genre);
    }

    // PUT: api/genre/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutGenre(int id, [FromBody] GenreDto dto)
    {
        if (id != dto.Id)
            return BadRequest(
                "L'ID de l'genre dans l'URL ne correspond pas à celui du corps de la requête."
            );

        var genre = new Genre(dto);
        _context.Entry(genre).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Genres.Any(a => a.Id == id))
                return NotFound($"Aucun genre trouvé avec l'ID {id}.");
            else
                throw;
        }

        return NoContent();
    }

    // DELETE: api/genre/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var genre = await _context.Genres.FindAsync(id);

        if (genre == null)
            return NotFound($"Aucun genre trouvé avec l'ID {id}.");

        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();

        return NoContent();
    }*/
}
