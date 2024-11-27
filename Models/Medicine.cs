using System;
using System.ComponentModel.DataAnnotations;
using AnimalClinicAPI.Models;

namespace AnimalClinicAPI.Models
{
    public class Medicine
    {
        public int Record_ID { get; set; } /*FK int not null*/     
        
        [MaxLength(100)] /*มี max 100 อักษร*/
        public string Medicine_name { get; set; } /*Varchar(100)*/  
        public int Medicine_amount { get; set; } /*int*/
        
        public MedicalRecord MedicalRecord { get; set; }
    }
}
