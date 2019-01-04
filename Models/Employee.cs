using System;  
using System.ComponentModel.DataAnnotations;

namespace sample1.Models
{
    public class Employee : BaseEntity
    {
        [Required]  
        public string Name { get; set; }  
        [Required]  
        public string City { get; set; }  
        [Required]  
        public string Department { get; set; }  
        [Required]  
        public int Salary {get;set;}
        public int DepartmentId{get; set;}
    }
}