using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RentasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Rentas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Renta>>> GetRentas()
        {
            return await _context.Rentas.ToListAsync();
        }

        // GET: api/Rentas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Renta>> GetRenta(int id)
        {
            var renta = await _context.Rentas.FindAsync(id);

            if (renta == null)
            {
                return NotFound();
            }

            return renta;
        }

        // PUT: api/Rentas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRenta(int id, Renta renta)
        {
            if (id != renta.Id)
            {
                return BadRequest();
            }

            _context.Entry(renta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Rentas
        [HttpPost]
        public async Task<ActionResult<Renta>> PostRenta(Renta renta)
        {
            _context.Rentas.Add(renta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRenta), new { id = renta.Id }, renta);
        }

        // DELETE: api/Rentas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRenta(int id)
        {
            var renta = await _context.Rentas.FindAsync(id);
            if (renta == null)
            {
                return NotFound();
            }

            _context.Rentas.Remove(renta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RentaExists(int id)
        {
            return _context.Rentas.Any(e => e.Id == id);
        }
    }
}
