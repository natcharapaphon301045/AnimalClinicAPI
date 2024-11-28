using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;  // เพิ่มการใช้งานสำหรับ ICollection
using AnimalClinicAPI.Models;

namespace AnimalClinicAPI.Models
{
    public class Pet
    {
        [Key] /*Primary Key*/
        public int Pet_ID { get; set; } /*PK int not null*/    
        public int Customer_ID { get; set; } /*FK int not null*/     

        [MaxLength(50)] /*มี max 50 อักษร*/
        public string Pet_Name { get; set; } /*Varchar(50)*/  
        
        [MaxLength(50)] /*มี max 50 อักษร*/
        public string Pet_Breed { get; set; } /*Varchar(50)*/
        
        [MaxLength(15)] /*มี max 15 อักษร*/
        public string Pet_Gender { get; set; } /*Varchar(15)*/
        
        public int Pet_Age { get; set; } /*int not null*/
        
        [MaxLength(15)] /*มี max 15 อักษร*/
        public string Pet_Color { get; set; } /*Varchar(15)*/
        
        public PetOwner PetOwner { get; set; }
        
        // เพิ่ม Property นี้เพื่อแสดงถึงความสัมพันธ์ One-to-Many กับ Appointment&Medicalrecord
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<MedicalRecord> MedicalRecords { get; set; }
    }
}
