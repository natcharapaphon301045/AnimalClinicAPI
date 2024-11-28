using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimalClinicAPI.Models;

namespace AnimalClinicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PetController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Pet
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pet>>> GetPets()
        {
            return await _context.Pets.ToListAsync();
        }

        // GET: api/Pet/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pet>> GetPet(int id)
        {
            var pet = await _context.Pets.FindAsync(id);

            if (pet == null)
            {
                return NotFound();
            }

            return pet;
        }

        // POST: api/Pet
        [HttpPost]
        public async Task<ActionResult<Pet>> PostPet(Pet pet)
        {
            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPet", new { id = pet.Pet_ID }, pet);
        }

        // PUT: api/Pet/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPet(int id, Pet pet)
        {
            if (id != pet.Pet_ID)
            {
                return BadRequest();
            }

            _context.Entry(pet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetExists(id))
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

        // DELETE: api/Pet/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePet(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }

            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PetExists(int id)
        {
            return _context.Pets.Any(e => e.Pet_ID == id);
        }
        // GET: api/Pet/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Pet>>> SearchPets(
            [FromQuery] string petName,
            [FromQuery] string ownerName,
            [FromQuery] string pet_Breed)
        {
            
            if (string.IsNullOrEmpty(petName) && string.IsNullOrEmpty(ownerName) && string.IsNullOrEmpty(pet_Breed))
            {
                return BadRequest("Please provide at least one search parameter.");
            }
            // ตั้งจุด Breakpoint
            // Query ข้อมูลจาก Database
            var query = _context.Pets.AsQueryable();

            if (!string.IsNullOrEmpty(petName))
                query = query.Where(p => p.Pet_Name.Contains(petName));

            if (!string.IsNullOrEmpty(ownerName))
                query = query.Where(p => p.PetOwner.Customer_firstname.Contains(ownerName) || p.PetOwner.Customer_lastname.Contains(ownerName));

            if (!string.IsNullOrEmpty(pet_Breed))
                query = query.Where(p => p.Pet_Breed.Contains(pet_Breed));

            var result = await query.ToListAsync();

            // ตรวจสอบว่ามีข้อมูลหรือไม่
            if (!result.Any())
            {
                return NotFound("No matching pets found.");
            }

            // Return Response 5 ค่า
            var response = result.Select(p => new
            {
                p.Pet_ID,
                p.Pet_Name,
                p.Pet_Breed,
                OwnerName = p.PetOwner.Customer_firstname + " " + p.PetOwner.Customer_lastname,
                p.Pet_Age
            });

            return Ok(response);
        }
    }
}
