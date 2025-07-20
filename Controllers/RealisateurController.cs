using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/realisateur")]
public class RealisateurController : ControllerBase
{
    private readonly DataContext _context;

    public RealisateurController(DataContext context)
    {
        _context = context;
    }

    // GET: api/realisateur
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RealisateurDto>>> GetRealisateurs()
    {
        var realisateursDto = await _context
            .Realisateurs.Select(a => new RealisateurDto(a))
            .ToListAsync();

        return realisateursDto;
    }

    // GET: api/realisateur/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<RealisateurDto>> GetRealisateur(int id)
    {
        var realisateur = await _context.Realisateurs.SingleOrDefaultAsync(a => a.Id == id);

        if (realisateur == null)
            return NotFound($"Aucun realisateur trouvé avec l'ID {id}.");

        return new RealisateurDto(realisateur);
    }

    // GET: api/realisateur/{id}/films
    [HttpGet("{id}/films")]
    public async Task<ActionResult<IEnumerable<FilmOutputDto>>> GetFilmsDeRealisateur(int id)
    {
        var realisateur = await _context.Realisateurs.FindAsync(id);
        if (realisateur == null)
            return NotFound($"Aucun realisateur trouvé avec l'ID {id}.");

        var films = await _context
            .Films.Include(f => f.Genres)
            .Include(f => f.Pays)
            .Include(f => f.Realisateurs)
            .Include(f => f.Realisateurs)
            .Include(f => f.Compositeurs)
            .ToListAsync();

        var filmsDeRealisateur = realisateur
            .GetFilms(films)
            .Select(f => new FilmOutputDto(f))
            .ToList();

        return filmsDeRealisateur;
    }

    // POST: api/realisateur
    [HttpPost]
    public async Task<ActionResult<Realisateur>> PostRealisateur([FromBody] RealisateurDto dto)
    {
        var realisateur = new Realisateur(dto);
        _context.Realisateurs.Add(realisateur);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetRealisateur), new { id = realisateur.Id }, realisateur);
    }

    // PUT: api/realisateur/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRealisateur(int id, [FromBody] RealisateurDto dto)
    {
        if (id != dto.Id)
            return BadRequest(
                "L'ID de l'realisateur dans l'URL ne correspond pas à celui du corps de la requête."
            );

        var realisateur = new Realisateur(dto);
        _context.Entry(realisateur).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Realisateurs.Any(a => a.Id == id))
                return NotFound($"Aucun realisateur trouvé avec l'ID {id}.");
            else
                throw;
        }

        return NoContent();
    }

    // DELETE: api/realisateur/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRealisateur(int id)
    {
        var realisateur = await _context.Realisateurs.FindAsync(id);

        if (realisateur == null)
            return NotFound($"Aucun realisateur trouvé avec l'ID {id}.");

        _context.Realisateurs.Remove(realisateur);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
