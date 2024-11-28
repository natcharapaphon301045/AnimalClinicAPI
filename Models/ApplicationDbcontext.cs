using Microsoft.EntityFrameworkCore;

namespace AnimalClinicAPI.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetOwner> PetOwners { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           if (!optionsBuilder.IsConfigured) // ตรวจสอบว่าไม่ได้ตั้งค่ามาก่อน
            {
                // เพิ่ม Encrypt=False ใน Connection String
                optionsBuilder.UseSqlServer("Server=WEIL-O14P110C;Database=VetweinaryClinic;Trusted_Connection=True;Encrypt=False;");
            }
        }
    }
}