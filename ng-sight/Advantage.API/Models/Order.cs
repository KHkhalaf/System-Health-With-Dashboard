using System;

namespace Advantage.API.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Customer customer { get; set; }
        public decimal total { get; set; }
        public DateTime Placed { get; set; }
        public DateTime? Completed { get; set; }
    }
}