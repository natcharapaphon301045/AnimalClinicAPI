using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // สำหรับการใช้งาน Validation Attributes
using AnimalClinicAPI.Models;  // เชื่อมโยงกับ Models ของโปรเจค

namespace AnimalClinicAPI.Models
{
    public class PetOwner
    {   
        [Key]  // Primary Key
        public int Customer_ID { get; set; }  // PK int, not null

        [Required]  // ทำให้ไม่สามารถเป็น null ได้
        [MaxLength(15)]  // Maximum length 15 อักษร
        public string Phonenumber { get; set; }  // หมายเลขโทรศัพท์

        [Required]  // ทำให้ไม่สามารถเป็น null ได้
        [MaxLength(50)]  // Maximum length 50 อักษร
        public string Customer_Firstname { get; set; }  // ชื่อลูกค้า

        [Required]  // ทำให้ไม่สามารถเป็น null ได้
        [MaxLength(50)]  // Maximum length 50 อักษร
        public string Customer_Lastname { get; set; }  // นามสกุลลูกค้า

        public ICollection<Pet> Pets { get; set; }
        // Navigation Property ที่เชื่อมโยงกับ Appointment
        public ICollection<Appointment> Appointments { get; set; }  // Customer สามารถมีหลาย Appointment
    }
}
