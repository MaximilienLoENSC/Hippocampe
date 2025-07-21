using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/pays")]
public class PaysController : ControllerBase
{
    private readonly DataContext _context;

    public PaysController(DataContext context)
    {
        _context = context;
    }

    // GET: api/pays
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PaysDto>>> GetPays()
    {
        var pays = await _context.Pays.Include(a => a.Films).ToListAsync();

        var paysDto = pays.Select(a => new PaysDto(a)).ToList();

        return Ok(paysDto);
    }

    // GET: api/pays/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<PaysDto>> GetPays(int id)
    {
        var pays = await _context.Pays.Include(a => a.Films).FirstOrDefaultAsync(a => a.Id == id);

        if (pays == null)
            return NotFound($"Aucun pays trouvé avec l'ID {id}.");

        return new PaysDto(pays);
    }

    // GET: api/pays/{id}/films
    [HttpGet("{id}/films")]
    public async Task<ActionResult<IEnumerable<FilmDto>>> GetFilmsDePays(int id)
    {
        var pays = await _context
            .Pays.Include(a => a.Films)
            .ThenInclude(f => f.Genres)
            .Include(a => a.Films)
            .ThenInclude(f => f.Pays)
            .Include(a => a.Films)
            .ThenInclude(f => f.Realisateurs)
            .Include(a => a.Films)
            .ThenInclude(f => f.Compositeurs)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (pays == null)
            return NotFound($"Aucun pays trouvé avec l'ID {id}.");

        var filmsDePays = pays.Films.Select(f => new FilmDto(f)).ToList();

        return Ok(filmsDePays);
    }

    /*
    // POST: api/pays
    [HttpPost]
    public async Task<ActionResult<Pays>> PostPays([FromBody] PaysDto dto)
    {
        var pays = new Pays(dto);
        _context.Pays.Add(pays);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPays), new { id = pays.Id }, pays);
    }

    // PUT: api/pays/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPays(int id, [FromBody] PaysDto dto)
    {
        if (id != dto.Id)
            return BadRequest(
                "L'ID de l'pays dans l'URL ne correspond pas à celui du corps de la requête."
            );

        var pays = new Pays(dto);
        _context.Entry(pays).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Pays.Any(a => a.Id == id))
                return NotFound($"Aucun pays trouvé avec l'ID {id}.");
            else
                throw;
        }

        return NoContent();
    }

    // DELETE: api/pays/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePays(int id)
    {
        var pays = await _context.Pays.FindAsync(id);

        if (pays == null)
            return NotFound($"Aucun pays trouvé avec l'ID {id}.");

        _context.Pays.Remove(pays);
        await _context.SaveChangesAsync();

        return NoContent();
    }*/
}
