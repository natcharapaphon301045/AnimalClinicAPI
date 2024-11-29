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
            [FromBody] PetOwner petOwner)
        {
            // ตรวจสอบว่าได้รับข้อมูลหรือไม่
            if (petOwner == null || 
                string.IsNullOrEmpty(petOwner.Customer_Firstname) ||
                string.IsNullOrEmpty(petOwner.Customer_Lastname) ||
                string.IsNullOrEmpty(petOwner.Phonenumber))
            {
                return BadRequest("Please provide all required fields: firstName, lastName, and phoneNumber.");
            }

            // บันทึกข้อมูล PetOwner ลงใน Database
            _context.PetOwner.Add(petOwner);
            await _context.SaveChangesAsync();

            // สร้าง Response ที่มี 5 ค่า
            var response = new
            {
                petOwner.Customer_ID,           // รหัสลูกค้า
                petOwner.Customer_Firstname,    // ชื่อ
                petOwner.Customer_Lastname,     // นามสกุล
                petOwner.Phonenumber,          // เบอร์โทร
                TotalPets = _context.Pet.Count(p => p.Customer_ID == petOwner.Customer_ID) // จำนวนสัตว์เลี้ยงของเจ้าของคนนี้
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
            var query = _context.PetOwner.AsQueryable();

            if (!string.IsNullOrEmpty(firstName))
                query = query.Where(po => po.Customer_Firstname.Contains(firstName));

            if (!string.IsNullOrEmpty(lastName))
                query = query.Where(po => po.Customer_Lastname.Contains(lastName));

            if (!string.IsNullOrEmpty(phoneNumber))
                query = query.Where(po => po.Phonenumber.Contains(phoneNumber));

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
                po.Customer_Firstname,
                po.Customer_Lastname,
                po.Phonenumber,
                TotalPets = _context.Pet.Count(p => p.Customer_ID == po.Customer_ID) // นับจำนวนสัตว์เลี้ยง
            });

            return Ok(response);
        }
    }
}
