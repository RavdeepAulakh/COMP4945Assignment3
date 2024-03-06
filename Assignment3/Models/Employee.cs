using System;
using System.ComponentModel.DataAnnotations;

namespace Assignment3.Models
{
    public class Employee : Person
    {
        [Key]
        public int EmployeeId { get; set; }

        public string Qualification { get; set; }
    }

}

