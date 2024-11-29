using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimalClinicAPI.Models;

namespace AnimalClinicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AppointmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Appointment/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<object>>> SearchAppointments(
            [FromQuery] DateTime? appointmentDate,
            [FromQuery] int? petId,
            [FromQuery] string statusAppointment)
        {
            if (!appointmentDate.HasValue && !petId.HasValue && string.IsNullOrEmpty(statusAppointment))
            {
                return BadRequest("Please provide at least one search parameter.");
            }

            var query = _context.Appointment
                .Include(a => a.PetOwner)
                .Include(a => a.Pet)
                .AsQueryable();

            if (appointmentDate.HasValue)
                query = query.Where(a => a.AppointmentDate.Date == appointmentDate.Value.Date);

            if (petId.HasValue)
                query = query.Where(a => a.Pet_ID == petId.Value);

            if (!string.IsNullOrEmpty(statusAppointment))
                query = query.Where(a => a.StatusAppointment.Contains(statusAppointment));

            var result = await query.ToListAsync();

            if (!result.Any())
            {
                return NotFound("No matching appointments found.");
            }

            var response = result.Select(a => new
            {
                a.Appointment_ID,
                a.Pet_ID,
                PetName = a.Pet.Pet_Name,
                CustomerName = a.PetOwner.Customer_Firstname + " " + a.PetOwner.Customer_Lastname,
                a.AppointmentDate,
                a.AppointmentTime,
                a.StatusAppointment
            });

            return Ok(response);
        }

        // POST: api/Appointment
        [HttpPost]
        public async Task<ActionResult<object>> PostAppointment(
            [FromQuery] int petId,
            [FromQuery] int customerId,
            [FromQuery] DateTime appointmentDate,
            [FromQuery] TimeSpan appointmentTime,
            [FromQuery] string statusAppointment)
        {
            if (petId <= 0 || customerId <= 0 || appointmentDate == default || appointmentTime == default || string.IsNullOrEmpty(statusAppointment))
            {
                return BadRequest("Please provide valid parameters: petId, customerId, appointmentDate, appointmentTime, and statusAppointment.");
            }

            var appointment = new Appointment
            {
                Pet_ID = petId,
                Customer_ID = customerId,
                AppointmentDate = appointmentDate,
                AppointmentTime = appointmentTime,
                StatusAppointment = statusAppointment
            };

            _context.Appointment.Add(appointment);
            await _context.SaveChangesAsync();

            var response = new
            {
                appointment.Appointment_ID,
                appointment.Pet_ID,
                appointment.Customer_ID,
                appointment.AppointmentDate,
                appointment.AppointmentTime,
                appointment.StatusAppointment
            };

            return CreatedAtAction(nameof(SearchAppointments), new { appointment.Appointment_ID }, response);
        }
    }
}
