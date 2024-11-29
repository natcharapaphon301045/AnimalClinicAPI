using Dapper; // ใช้สำหรับ Query ข้อมูลแบบง่าย
using Microsoft.Data.SqlClient; // ใช้ SqlConnection สำหรับเชื่อมต่อฐานข้อมูล
using AnimalClinicAPI.Models;

namespace YourProject.Services
{
    public class MedicalRecordService
    {
        private readonly string _connectionString;

        public MedicalRecordService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AnimalClinicDB");
        }

        // ตัวอย่าง Method ดึงข้อมูลทั้งหมด
        public IEnumerable<MedicalRecord> GetAllRecords()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM MedicalRecord";
                return connection.Query<MedicalRecord>(query);
            }
        }

        // ตัวอย่าง Method เพิ่มข้อมูลใหม่
        public int AddMedicalRecord(MedicalRecord record)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = @"
                    INSERT INTO MedicalRecord (Pet_ID, StatusAppointment, PetWeight, AppointmentDate)
                    VALUES (@Pet_ID, @StatusAppointment, @PetWeight, @AppointmentDate)";
                return connection.Execute(query, record);
            }
        }
    }
}
