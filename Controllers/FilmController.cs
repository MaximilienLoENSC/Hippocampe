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

    // GET: api/films
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Film>>> GetFilms()
    {
        // Get items
        var films = _context
            .Films.Include(f => f.Genres)
            .Include(f => f.Pays)
            .Include(f => f.Acteurs)
            .Include(f => f.Realisateurs)
            .Include(f => f.Compositeurs)
            .ToListAsync();

        return await films;
    }
}
