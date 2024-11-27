using System;
using System.ComponentModel.DataAnnotations;
using AnimalClinicAPI.Models;

namespace AnimalClinicAPI.Models
{
    public class Appointment
    {
        [Key] /*Primary Key*/
        public int Appointment_ID { get; set; } /*PK int not null*/    
        public int Pet_ID { get; set; } /*FK int not null*/     
        public int Customer_ID { get; set; } /*FK int not null*/

        [MaxLength(50)] /*มี max 50 อักษร*/
        public string StatusAppointment { get; set; } /*Varchar(50)*/  

        public DateTime AppointmentDate { get; set; } /*Date*/
        public TimeSpan AppointmentTime { get; set; } /*เวลา (TimeSpan)*/

        // 
        public Pet Pet { get; set; }  
        public PetOwner PetOwner { get; set; }
    }
}
