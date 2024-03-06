using System;
using System.ComponentModel.DataAnnotations;

namespace Assignment3.Models
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }

        public string ServiceName { get; set; }

        public List<Customer> Customers { get; set; } = new List<Customer>();
    }

}

