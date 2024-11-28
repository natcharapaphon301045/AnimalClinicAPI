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

        // GET: api/Pet/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<object>>> SearchPets(
            [FromQuery] string petName,
            [FromQuery] string breed,
            [FromQuery] string ownerName)
        {
            if (string.IsNullOrEmpty(petName) && string.IsNullOrEmpty(breed) && string.IsNullOrEmpty(ownerName))
            {
                return BadRequest("Please provide at least one search parameter.");
            }

            // Query ข้อมูลจากฐานข้อมูล
            var query = _context.Pets.Include(p => p.PetOwner).AsQueryable();

            if (!string.IsNullOrEmpty(petName))
                query = query.Where(p => p.Pet_Name.Contains(petName));

            if (!string.IsNullOrEmpty(breed))
                query = query.Where(p => p.Pet_Breed.Contains(breed));

            if (!string.IsNullOrEmpty(ownerName))
                query = query.Where(p => p.PetOwner.Customer_firstname.Contains(ownerName) || 
                                         p.PetOwner.Customer_lastname.Contains(ownerName));

            var result = await query.ToListAsync();

            // ตรวจสอบว่ามีข้อมูลหรือไม่
            if (!result.Any())
            {
                return NotFound("No matching pets found.");
            }

            // Response 5 ค่า
            var response = result.Select(p => new
            {
                p.Pet_ID,
                p.Pet_Name,
                p.Pet_Breed,
                p.Pet_Age,
                OwnerName = p.PetOwner.Customer_firstname + " " + p.PetOwner.Customer_lastname
            });

            return Ok(response);
        }

        // POST: api/Pet
        [HttpPost]
        public async Task<ActionResult<object>> PostPet(
            [FromQuery] string petName,
            [FromQuery] string breed,
            [FromQuery] int age,
            [FromQuery] int customerId)
        {
            // ตรวจสอบว่าพารามิเตอร์ครบถ้วน
            if (string.IsNullOrEmpty(petName) || string.IsNullOrEmpty(breed) || age <= 0 || customerId <= 0)
            {
                return BadRequest("Please provide valid parameters: petName, breed, age, and customerId.");
            }

            // สร้างข้อมูลสัตว์เลี้ยงใหม่
            var pet = new Pet
            {
                Pet_Name = petName,
                Pet_Breed = breed,
                Pet_Age = age,
                Customer_ID = customerId
            };

            // บันทึกลงฐานข้อมูล
            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();

            // Response 5 ค่า
            var response = new
            {
                pet.Pet_ID,
                pet.Pet_Name,
                pet.Pet_Breed,
                pet.Pet_Age,
                CustomerName = _context.PetOwners
                                .Where(po => po.Customer_ID == customerId)
                                .Select(po => po.Customer_firstname + " " + po.Customer_lastname)
                                .FirstOrDefault()
            };

            return CreatedAtAction(nameof(SearchPets), new { pet.Pet_ID }, response);
        }
    }
}
