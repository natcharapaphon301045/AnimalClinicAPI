using System;
using System.ComponentModel.DataAnnotations;
using AnimalClinicAPI.Models;

namespace AnimalClinicAPI.Models
{
    public class PetOwner
    {   
        [Key] /*Primary Key*/
        public int Customer_ID { get; set; }/*PK int not null*/     

        [MaxLength(15)] /*มี max 50 อักษร*/
        public string Phone_number { get; set; }/*null*/

        [MaxLength(50)]
        public string Customer_firstname { get; set;}/*null*/

        [MaxLength(50)]
        public string Customer_lastname {get; set;}/*null*/       
        
        
    }
}
