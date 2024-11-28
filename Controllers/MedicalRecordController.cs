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

        // GET: api/MedicalRecord
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalRecord>>> GetMedicalRecords()
        {
            return await _context.MedicalRecords.ToListAsync();
        }

        // GET: api/MedicalRecord/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalRecord>> GetMedicalRecord(int id)
        {
            var medicalRecord = await _context.MedicalRecords.FindAsync(id);

            if (medicalRecord == null)
            {
                return NotFound();
            }

            return medicalRecord;
        }

        // POST: api/MedicalRecord
        [HttpPost]
        public async Task<ActionResult<MedicalRecord>> PostMedicalRecord(MedicalRecord medicalRecord)
        {
            _context.MedicalRecords.Add(medicalRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMedicalRecord", new { id = medicalRecord.Record_ID }, medicalRecord);
        }

        // PUT: api/MedicalRecord/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicalRecord(int id, MedicalRecord medicalRecord)
        {
            if (id != medicalRecord.Record_ID)
            {
                return BadRequest();
            }

            _context.Entry(medicalRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicalRecordExists(id))
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

        // DELETE: api/MedicalRecord/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicalRecord(int id)
        {
            var medicalRecord = await _context.MedicalRecords.FindAsync(id);
            if (medicalRecord == null)
            {
                return NotFound();
            }

            _context.MedicalRecords.Remove(medicalRecord);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MedicalRecordExists(int id)
        {
            return _context.MedicalRecords.Any(e => e.Record_ID == id);
        }// GET: api/MedicalRecord/search
        
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<MedicalRecord>>> SearchMedicalRecords(
            [FromQuery] DateTime? medicalDate,
            [FromQuery] string treatmentType)
        {
            // ตรวจสอบว่ามี Query Parameters หรือไม่
            if (!medicalDate.HasValue && string.IsNullOrEmpty(treatmentType))
            {
                return BadRequest("Please provide at least one search parameter.");
            }

            // Query ข้อมูลจาก Database
            var query = _context.MedicalRecords.AsQueryable();

            // กรองตามวันที่การรักษา (ถ้ามี)
            if (medicalDate.HasValue)
                query = query.Where(m => m.Medical_Date.Date == medicalDate.Value.Date);

            // กรองตามประเภทการรักษา (ถ้ามี)
            if (!string.IsNullOrEmpty(treatmentType))
                query = query.Where(m => m.TreatmentType.Contains(treatmentType));

            var result = await query.ToListAsync();

            // ตรวจสอบว่ามีข้อมูลหรือไม่
            if (!result.Any())
            {
                return NotFound("No matching medical records found.");
            }

            // Return Response พร้อมข้อมูลที่ต้องการ
            var response = result.Select(m => new
            {
                m.Record_ID,
                m.Pet_ID,
                m.TreatmentType,
                m.TreatmentDetail,
                m.Medical_Date,
                m.Pet_Weight
            });

            return Ok(response);
        }


    }
}
