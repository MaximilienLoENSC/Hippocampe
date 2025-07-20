using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/compositeur")]
public class CompositeurController : ControllerBase
{
    private readonly DataContext _context;

    public CompositeurController(DataContext context)
    {
        _context = context;
    }

    // GET: api/compositeur
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompositeurDto>>> GetCompositeurs()
    {
        var compositeursDto = await _context
            .Compositeurs.Select(a => new CompositeurDto(a))
            .ToListAsync();

        return compositeursDto;
    }

    // GET: api/compositeur/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<CompositeurDto>> GetCompositeur(int id)
    {
        var compositeur = await _context.Compositeurs.SingleOrDefaultAsync(a => a.Id == id);

        if (compositeur == null)
            return NotFound($"Aucun compositeur trouvé avec l'ID {id}.");

        return new CompositeurDto(compositeur);
    }

    // GET: api/compositeur/{id}/films
    [HttpGet("{id}/films")]
    public async Task<ActionResult<IEnumerable<FilmOutputDto>>> GetFilmsDeCompositeur(int id)
    {
        var compositeur = await _context.Compositeurs.FindAsync(id);
        if (compositeur == null)
            return NotFound($"Aucun compositeur trouvé avec l'ID {id}.");

        var films = await _context
            .Films.Include(f => f.Genres)
            .Include(f => f.Pays)
            .Include(f => f.Realisateurs)
            .Include(f => f.Compositeurs)
            .Include(f => f.Compositeurs)
            .ToListAsync();

        var filmsDeCompositeur = compositeur
            .GetFilms(films)
            .Select(f => new FilmOutputDto(f))
            .ToList();

        return filmsDeCompositeur;
    }

    // POST: api/compositeur
    [HttpPost]
    public async Task<ActionResult<Compositeur>> PostCompositeur([FromBody] CompositeurDto dto)
    {
        var compositeur = new Compositeur(dto);
        _context.Compositeurs.Add(compositeur);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCompositeur), new { id = compositeur.Id }, compositeur);
    }

    // PUT: api/compositeur/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCompositeur(int id, [FromBody] CompositeurDto dto)
    {
        if (id != dto.Id)
            return BadRequest(
                "L'ID de l'compositeur dans l'URL ne correspond pas à celui du corps de la requête."
            );

        var compositeur = new Compositeur(dto);
        _context.Entry(compositeur).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Compositeurs.Any(a => a.Id == id))
                return NotFound($"Aucun compositeur trouvé avec l'ID {id}.");
            else
                throw;
        }

        return NoContent();
    }

    // DELETE: api/compositeur/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompositeur(int id)
    {
        var compositeur = await _context.Compositeurs.FindAsync(id);

        if (compositeur == null)
            return NotFound($"Aucun compositeur trouvé avec l'ID {id}.");

        _context.Compositeurs.Remove(compositeur);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
