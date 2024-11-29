using Microsoft.EntityFrameworkCore;
using AnimalClinicAPI.Models;

namespace AnimalClinicAPI.Models
{
    public class ApplicationDbContext : DbContext
    {
            public DbSet<PetOwner> PetOwner { get; set; }
            public DbSet<Pet> Pet { get; set; }
            public DbSet<MedicalRecord> MedicalRecord { get; set; }
            public DbSet<Appointment> Appointment { get; set; }
       
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        // การกำหนดความสัมพันธ์ (OnModelCreating) ตามปกติ
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PetOwner>().ToTable("PetOwners");
            modelBuilder.Entity<Pet>().ToTable("Pets");
            modelBuilder.Entity<MedicalRecord>().ToTable("MedicalRecords");
            modelBuilder.Entity<Appointment>().ToTable("Appointments");

            modelBuilder.Entity<Pet>()
                .HasOne(p => p.PetOwner)  // Pet มี PetOwner
                .WithMany(po => po.Pets)  // PetOwner มีหลาย Pet
                .HasForeignKey(p => p.Customer_ID)  // FK ที่เชื่อมโยงจาก Pet ไปยัง PetOwner
                .OnDelete(DeleteBehavior.Restrict); 

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
