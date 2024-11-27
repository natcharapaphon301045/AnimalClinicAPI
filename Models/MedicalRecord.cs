using System;
using System.ComponentModel.DataAnnotations;
using AnimalClinicAPI.Models;

namespace AnimalClinicAPI.Models
{
    public class MedicalRecord
    {
        [Key] /*Primary Key*/
        public int Record_ID { get; set; }/*PK int not null*/    
        public int Pet_ID { get; set; }/*FK int not null*/     

        [MaxLength(100)] /*มี max 200 อักษร*/
        public string TreatmentType { get; set; }/*Varchar(200)*/  
        [MaxLength(200)] /*มี max 200 อักษร*/
        public string TreatmentDetail { get; set; }/*Varchar(200)*/
        [Range(0,999.99)]
        public decimal Pet_Weight{ get; set; }/*Decimal(2.5)*/
        public DateTime Medical_Date { get; set; }/*DateTime null*/
        public bool Medicineget { get; set; }/*bit (SQL) */
        
        public Pet Pet { get; set; }
    }
}
