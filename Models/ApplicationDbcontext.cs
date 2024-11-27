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
        public DbSet<Medicine> Medicines { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           if (!optionsBuilder.IsConfigured) // ตรวจสอบว่าไม่ได้ตั้งค่ามาก่อน
            {
                // เพิ่ม Encrypt=False ใน Connection String
                optionsBuilder.UseSqlServer("Server=WEIL-O14P110C;Database=VeterinaryClinic;Trusted_Connection=True;Encrypt=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // กำหนดให้ไม่อนุญาตให้ลบข้อมูลในตารางที่เกี่ยวข้อง (NoAction)
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Pet)
                .WithMany()  // ถ้ามีความสัมพันธ์ One-to-Many
                .HasForeignKey(a => a.Pet_ID)
                .OnDelete(DeleteBehavior.Restrict); // เปลี่ยนเป็น Restrict หรือ NoAction

            modelBuilder.Entity<MedicalRecord>()
                .HasOne(m => m.Pet)
                .WithMany()
                .HasForeignKey(m => m.Pet_ID)
                .OnDelete(DeleteBehavior.Restrict); // เปลี่ยนเป็น Restrict หรือ NoAction

            // หากไม่มีการใช้ PK ใน Medicine, ก็ให้ระบุ HasNoKey() ด้วย
            modelBuilder.Entity<Medicine>().HasNoKey();
        }

    }
}