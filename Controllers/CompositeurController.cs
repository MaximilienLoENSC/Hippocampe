using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/compositeur")]
public class CompositeurController : ControllerBase
{
    private readonly FilmContext _context;

    public CompositeurController(FilmContext context)
    {
        _context = context;
    }

    // GET: api/compositeur
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompositeurDto>>> GetCompositeurs()
    {
        var compositeurs = await _context.Compositeurs.Include(a => a.Films).ToListAsync();

        var compositeursDto = compositeurs.Select(a => new CompositeurDto(a)).ToList();

        return Ok(compositeursDto);
    }

    // GET: api/compositeur/{nom}
    [HttpGet("{nom}")]
    public async Task<ActionResult<CompositeurDto>> GetCompositeur(string nom)
    {
        var compositeur = await _context
            .Compositeurs.Include(a => a.Films)
            .FirstOrDefaultAsync(a => a.Nom == nom);

        if (compositeur == null)
            return NotFound($"Aucun compositeur trouvé avec le nom {nom}.");

        return new CompositeurDto(compositeur);
    }

    // GET: api/compositeur/{nom}/films
    [HttpGet("{nom}/films")]
    public async Task<ActionResult<IEnumerable<FilmDto>>> GetFilmsDeCompositeur(string nom)
    {
        var compositeur = await _context
            .Compositeurs.Include(a => a.Films)
            .ThenInclude(f => f.Genres)
            .Include(a => a.Films)
            .ThenInclude(f => f.Pays)
            .Include(a => a.Films)
            .ThenInclude(f => f.Realisateurs)
            .Include(a => a.Films)
            .ThenInclude(f => f.Compositeurs)
            .FirstOrDefaultAsync(a => a.Nom == nom);

        if (compositeur == null)
            return NotFound($"Aucun compositeur trouvé avec le nom {nom}.");

        var filmsDeCompositeur = compositeur.Films.Select(f => new FilmDto(f)).ToList();

        return Ok(filmsDeCompositeur);
    }

    /*
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
    }*/
}
