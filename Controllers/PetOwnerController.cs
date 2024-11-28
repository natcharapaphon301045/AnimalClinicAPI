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

        [HttpPost]
        public async Task<ActionResult<object>> PostPetOwner(
            [FromQuery] string firstName,
            [FromQuery] string lastName,
            [FromQuery] string phoneNumber)
        {
            // ตรวจสอบว่าพารามิเตอร์ครบหรือไม่
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(phoneNumber))
            {
                return BadRequest("Please provide all required parameters: firstName, lastName, and phoneNumber.");
            }

            // สร้าง PetOwner ใหม่
            var petOwner = new PetOwner
            {
                Customer_firstname = firstName,
                Customer_lastname = lastName,
                Phone_number = phoneNumber
            };

            // บันทึกลง Database
            _context.PetOwners.Add(petOwner);
            await _context.SaveChangesAsync();

            // Response 5 ค่า
            var response = new
            {
                petOwner.Customer_ID,           // รหัสลูกค้า
                petOwner.Customer_firstname,    // ชื่อ
                petOwner.Customer_lastname,     // นามสกุล
                petOwner.Phone_number,          // เบอร์โทร
                TotalPets = _context.Pets.Count(p => p.Customer_ID == petOwner.Customer_ID) // จำนวนสัตว์เลี้ยงของเจ้าของคนนี้
            };

            return Ok(response);
        }


        // GET: api/PetOwners/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<object>>> SearchPetOwners(
            [FromQuery] string firstName,
            [FromQuery] string lastName,
            [FromQuery] string phoneNumber)
        {
            // ตรวจสอบว่ามีการส่งค่า Parameter อย่างน้อย 1 ตัว
            if (string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName) && string.IsNullOrEmpty(phoneNumber))
            {
                return BadRequest("Please provide at least one search parameter.");
            }

            // Query ข้อมูลจาก Database
            var query = _context.PetOwners.AsQueryable();

            if (!string.IsNullOrEmpty(firstName))
                query = query.Where(po => po.Customer_firstname.Contains(firstName));

            if (!string.IsNullOrEmpty(lastName))
                query = query.Where(po => po.Customer_lastname.Contains(lastName));

            if (!string.IsNullOrEmpty(phoneNumber))
                query = query.Where(po => po.Phone_number.Contains(phoneNumber));

            var result = await query.ToListAsync();

            // ตรวจสอบว่ามีข้อมูลหรือไม่
            if (!result.Any())
            {
                return NotFound("No matching pet owners found.");
            }

            // Return Response 5 ค่า
            var response = result.Select(po => new
            {
                po.Customer_ID,
                po.Customer_firstname,
                po.Customer_lastname,
                po.Phone_number,
                TotalPets = _context.Pets.Count(p => p.Customer_ID == po.Customer_ID) // นับจำนวนสัตว์เลี้ยง
            });

            return Ok(response);
        }
    }
}
