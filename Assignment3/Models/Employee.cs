using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment3.Models
{
    public class Employee : Person
    {
        [Key]
        public int EmployeeId { get; set; }

        // Remove ServiceIds property, as it's not needed

        public int? ServiceId { get; set; }

        // Remove Services property if it's not needed

        // Navigation property to access the service
        public Service? Service { get; set; }
    }
}
