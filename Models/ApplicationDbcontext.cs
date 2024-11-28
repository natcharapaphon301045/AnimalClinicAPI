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
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=WEIL-O14P110C;Database=VetweinaryClinic;Trusted_Connection=True;Encrypt=False;");
            }
        }

        // แก้ไขการกำหนดความสัมพันธ์
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // กำหนดความสัมพันธ์ของ Appointment กับ Pet
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Pet)  // Pet -> Appointment
                .WithMany(p => p.Appointments) // Pet สามารถมีหลาย Appointment
                .HasForeignKey(a => a.Pet_ID);  // กำหนดว่า Pet_ID เป็น Foreign Key ใน Appointment
                
            // กำหนดความสัมพันธ์ของ Appointment กับ Customer (PetOwner)
             modelBuilder.Entity<Appointment>()
                .HasOne(a => a.PetOwner)  // Customer -> Appointment
                .WithMany(c => c.Appointments) // Customer สามารถมีหลาย Appointment
                .HasForeignKey(a => a.Customer_ID);  // กำหนดว่า Customer_ID เป็น Foreign Key ใน Appointment

            // กำหนดความสัมพันธ์ของ MedicalRecord กับ Pet
            modelBuilder.Entity<MedicalRecord>()
                .HasOne(m => m.Pet)  // Pet -> MedicalRecord
                .WithMany(p => p.MedicalRecords) // Pet สามารถมีหลาย MedicalRecord
                .HasForeignKey(m => m.Pet_ID); // กำหนดว่า Pet_ID เป็น Foreign Key ใน MedicalRecord
        }

    }
}
