using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment3.Models
{
    public class Customer : Person
    {
        [Key]
        public int CustomerId { get; set; }

        public int? ServiceId { get; set; }

        [NotMapped]
        public List<int> ServiceIds { get; set; }

        public List<Service> Services { get; set; } = new List<Service>();
    }

}

