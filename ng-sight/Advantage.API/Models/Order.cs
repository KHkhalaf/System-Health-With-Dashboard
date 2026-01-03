using System;

namespace Advantage.API.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Customer customer { get; set; }
        public Delivery delivery { get; set; }
        public int? DeliveryId { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime Placed { get; set; }
        public DateTime? Completed { get; set; }
    }
}