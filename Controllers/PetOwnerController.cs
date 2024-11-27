using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimalClinicAPI.Models;  // เปลี่ยนตาม namespace ของคุณ

namespace AnimalClinicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetOwnersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PetOwnersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PetOwners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetOwner>>> GetPetOwners()
        {
            return await _context.PetOwners.ToListAsync();
        }

        // GET: api/PetOwners/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PetOwner>> GetPetOwner(int id)
        {
            var petOwner = await _context.PetOwners.FindAsync(id);

            if (petOwner == null)
            {
                return NotFound();
            }

            return petOwner;
        }

        // POST: api/PetOwners
        [HttpPost]
        public async Task<ActionResult<PetOwner>> PostPetOwner(PetOwner petOwner)
        {
            _context.PetOwners.Add(petOwner);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPetOwner), new { id = petOwner.Customer_ID }, petOwner);
        }

        // PUT: api/PetOwners/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPetOwner(int id, PetOwner petOwner)
        {
            if (id != petOwner.Customer_ID)
            {
                return BadRequest();
            }

            _context.Entry(petOwner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetOwnerExists(id))
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

        // DELETE: api/PetOwners/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePetOwner(int id)
        {
            var petOwner = await _context.PetOwners.FindAsync(id);
            if (petOwner == null)
            {
                return NotFound();
            }

            _context.PetOwners.Remove(petOwner);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PetOwnerExists(int id)
        {
            return _context.PetOwners.Any(e => e.Customer_ID == id);
        }
    }
}
