using System;
namespace Assignment3.Models
{
	public class EmployeeService
	{
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;

        public int ServiceId { get; set; }
        public Service Service { get; set; } = null!;
    }
}

