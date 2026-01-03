using System;

namespace Advantage.API.Models
{
    public class Delivery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Vehicle { get; set; }
        public string LicensePlate { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime? AssignedDate { get; set; }
    }
}

