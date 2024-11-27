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
        }
    }
}
