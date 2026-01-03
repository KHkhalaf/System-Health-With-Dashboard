using Microsoft.EntityFrameworkCore;

namespace Advantage.API.Models
{
    public class ApiContext:DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options):base(options){}

        public DbSet<Customer> customers{get; set;}
        public DbSet<Order> orders{get; set;}
        public DbSet<Server> Servers{get; set;}
        public DbSet<Delivery> deliveries{get; set;}
    }
}