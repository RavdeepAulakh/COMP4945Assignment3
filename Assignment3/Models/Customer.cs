using System;
using System.ComponentModel.DataAnnotations;

namespace Assignment3.Models
{
    public class Customer : Person
    {
        [Key]
        public int CustomerId { get; set; }

        public List<Service> Services { get; set; } = new List<Service>();
    }

}

