using Microsoft.EntityFrameworkCore;

namespace AnimalClinicAPI.Models
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor นี้ไม่จำเป็นต้องใช้ IConfiguration
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetOwner> PetOwners { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        // การกำหนดความสัมพันธ์ (OnModelCreating) ตามปกติ
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Pet)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.Pet_ID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.PetOwner)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.Customer_ID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MedicalRecord>()
                .HasOne(m => m.Pet)
                .WithMany(p => p.MedicalRecords)
                .HasForeignKey(m => m.Pet_ID);
        }
    }
}
