using Advantage.API.Models;
using System.Collections.Generic;
using System.Linq;

namespace Advantage.API
{
    public class DataSeed
    {
        private readonly ApiContext _ctx;

        public DataSeed(ApiContext ctx)
        {
            _ctx = ctx;
        }

        private static List<string> states = Helpers.states;

        public void SeedData(int ncustomers, int norders)
        {
            if (!_ctx.customers.Any())
            {
                Seedcustomers(ncustomers);
                _ctx.SaveChanges();
            }

            if (!_ctx.orders.Any())
            {
                Seedorders(norders);
                _ctx.SaveChanges();
            }

            if (!_ctx.Servers.Any())
            {
                SeedServers();
                _ctx.SaveChanges();
            }
        }

        internal void Seedcustomers(int n)
        {
            var customers = BuildcustomerList(n);

            foreach (var customer in customers)
            {
                _ctx.customers.Add(customer);
            }
        }

        internal void Seedorders(int n)
        {
            var orders = BuildOrderList(n);

            foreach (var order in orders)
            {
                _ctx.orders.Add(order);
            }
        }

        internal void SeedServers()
        {
            var servers = BuildServerList();

            foreach (var server in servers)
            {
                _ctx.Servers.Add(server);
            }
        }

        internal static List<Customer> BuildcustomerList(int n)
        {
            var customers = new List<Customer>();

            for (var i = 1; i <= n; i++)
            {
                var name = Helpers.MakeCustomerName();

                customers.Add(new Customer
                {
                    Name = name,
                    State = Helpers.GetRandom(states),
                    Email = Helpers.MakeEmail(name)
                });
            }

            return customers;
        }

        internal List<Order> BuildOrderList(int n)
        {
            var orders = new List<Order>();

            for (var i = 1; i <= n; i++)
            {
                var placed = Helpers.GetRandOrderPlaced();
                var completed = Helpers.GetRandOrderCompleted(placed);

                orders.Add(new Order
                {
                    customer = Helpers.GetRandomCustomer(_ctx),
                    OrderTotal = Helpers.GetRandomOrderTotal(),
                    Placed = placed,
                    Completed = completed
                });
            }

            return orders;
        }

        internal static List<Server> BuildServerList()
        {
            return new List<Server>()
            {
                new Server
                {
                    Name = "Dev-Web",
                    isOnline = true
                },

                new Server
                {
                    Name = "Dev-Analysis",
                    isOnline = true
                },

                new Server
                {
                    Name = "Dev-Mail",
                    isOnline = true
                },

                new Server
                {
                    Name = "QA-Web",
                    isOnline = true
                },

                new Server
                {
                    Name = "QA-Analysis",
                    isOnline = true
                },

                new Server
                {
                    Name = "QA-Mail",
                    isOnline = true
                },

                new Server
                {
                    Name = "Prod-Web",
                    isOnline = true
                },

                new Server
                {
                    Name = "Prod-Analysis",
                    isOnline = true
                },

                new Server
                {
                    Name = "Prod-Mail",
                    isOnline = true
                },
            };
        }
    }
}