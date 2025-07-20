using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/acteur")]
public class ActeurController : ControllerBase
{
    private readonly DataContext _context;

    public ActeurController(DataContext context)
    {
        _context = context;
    }

    // GET: api/acteur
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ActeurDto>>> GetActeurs()
    {
        var acteursDto = await _context.Acteurs.Select(a => new ActeurDto(a)).ToListAsync();

        return acteursDto;
    }

    // GET: api/acteur/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ActeurDto>> GetActeur(int id)
    {
        var acteur = await _context.Acteurs.SingleOrDefaultAsync(a => a.Id == id);

        if (acteur == null)
            return NotFound($"Aucun acteur trouvé avec l'ID {id}.");

        return new ActeurDto(acteur);
    }

    // GET: api/acteur/{id}/films
    [HttpGet("{id}/films")]
    public async Task<ActionResult<IEnumerable<FilmOutputDto>>> GetFilmsDeActeur(int id)
    {
        var acteur = await _context.Acteurs.FindAsync(id);
        if (acteur == null)
            return NotFound($"Aucun acteur trouvé avec l'ID {id}.");

        var films = await _context
            .Films.Include(f => f.Genres)
            .Include(f => f.Pays)
            .Include(f => f.Realisateurs)
            .Include(f => f.Acteurs)
            .Include(f => f.Compositeurs)
            .ToListAsync();

        var filmsDeActeur = acteur.GetFilms(films).Select(f => new FilmOutputDto(f)).ToList();

        return filmsDeActeur;
    }

    // POST: api/acteur
    [HttpPost]
    public async Task<ActionResult<Acteur>> PostActeur([FromBody] ActeurDto dto)
    {
        var acteur = new Acteur(dto);
        _context.Acteurs.Add(acteur);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetActeur), new { id = acteur.Id }, acteur);
    }

    // PUT: api/acteur/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutActeur(int id, [FromBody] ActeurDto dto)
    {
        if (id != dto.Id)
            return BadRequest(
                "L'ID de l'acteur dans l'URL ne correspond pas à celui du corps de la requête."
            );

        var acteur = new Acteur(dto);
        _context.Entry(acteur).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Acteurs.Any(a => a.Id == id))
                return NotFound($"Aucun acteur trouvé avec l'ID {id}.");
            else
                throw;
        }

        return NoContent();
    }

    // DELETE: api/acteur/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActeur(int id)
    {
        var acteur = await _context.Acteurs.FindAsync(id);

        if (acteur == null)
            return NotFound($"Aucun acteur trouvé avec l'ID {id}.");

        _context.Acteurs.Remove(acteur);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
