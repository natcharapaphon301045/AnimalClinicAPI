using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimalClinicAPI.Models;

namespace AnimalClinicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MedicalRecordController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MedicalRecord/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<object>>> SearchMedicalRecords(
            [FromQuery] int? petId,
            [FromQuery] DateTime? medicalDate,
            [FromQuery] string treatmentType)
        {
            // ตรวจสอบว่ามี Query Parameters หรือไม่
            if (!petId.HasValue && !medicalDate.HasValue && string.IsNullOrEmpty(treatmentType))
            {
                return BadRequest("Please provide at least one search parameter.");
            }

            // Query ข้อมูลจากฐานข้อมูล
            var query = _context.MedicalRecords.AsQueryable();

            if (petId.HasValue)
                query = query.Where(m => m.Pet_ID == petId.Value);

            if (medicalDate.HasValue)
                query = query.Where(m => m.Medical_Date.Date == medicalDate.Value.Date);

            if (!string.IsNullOrEmpty(treatmentType))
                query = query.Where(m => m.TreatmentType.Contains(treatmentType));

            var result = await query.ToListAsync();

            // ตรวจสอบว่ามีข้อมูลหรือไม่
            if (!result.Any())
            {
                return NotFound("No matching medical records found.");
            }

            // Response 5 ค่า
            var response = result.Select(m => new
            {
                m.Record_ID,
                m.Pet_ID,
                m.TreatmentType,
                m.TreatmentDetail,
                m.Medical_Date
            });

            return Ok(response);
        }

        // POST: api/MedicalRecord
        [HttpPost]
        public async Task<ActionResult<object>> PostMedicalRecord(
            [FromQuery] int petId,
            [FromQuery] string treatmentType,
            [FromQuery] string treatmentDetail,
            [FromQuery] DateTime medicalDate,
            [FromQuery] decimal petWeight)
        {
            // ตรวจสอบว่าข้อมูลครบถ้วน
            if (petId <= 0 || string.IsNullOrEmpty(treatmentType) || string.IsNullOrEmpty(treatmentDetail) || medicalDate == default || petWeight <= 0)
            {
                return BadRequest("Please provide valid parameters: petId, treatmentType, treatmentDetail, medicalDate, and petWeight.");
            }

            // สร้างข้อมูลใหม่
            var medicalRecord = new MedicalRecord
            {
                Pet_ID = petId,
                TreatmentType = treatmentType,
                TreatmentDetail = treatmentDetail,
                Medical_Date = medicalDate,
                Pet_Weight = petWeight
            };

            _context.MedicalRecords.Add(medicalRecord);
            await _context.SaveChangesAsync();

            // Response 5 ค่า
            var response = new
            {
                medicalRecord.Record_ID,
                medicalRecord.Pet_ID,
                medicalRecord.TreatmentType,
                medicalRecord.TreatmentDetail,
                medicalRecord.Medical_Date
            };

            return CreatedAtAction(nameof(SearchMedicalRecords), new { medicalRecord.Record_ID }, response);
        }
    }
}
