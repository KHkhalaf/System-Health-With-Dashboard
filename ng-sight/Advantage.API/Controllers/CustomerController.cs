using Advantage.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Advantage.API.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly ApiContext _ctx;

        public CustomerController(ApiContext ctx)
        {
            _ctx = ctx;
        }

        // GET api/customer/pageNumber/pageSize
        [HttpGet("{pageIndex:int}/{pageSize:int}")]
        public IActionResult Get(int pageIndex, int pageSize)
        {
            var data = _ctx.customers.OrderBy(c => c.Id);
            var page = new PaginatedResponse<Customer>(data, pageIndex, pageSize);

            var totalCount = data.Count();
            var totalPages = Math.Ceiling((double)totalCount / pageSize);

            var response = new
            {
                Page = page,
                TotalPages = totalPages
            };

            return Ok(response);
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetCustomer")]
        public Customer Get(int id)
        {
            return _ctx.customers.Find(id);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post(Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }

            _ctx.customers.Add(customer);
            _ctx.SaveChanges();

            return CreatedAtRoute("GetCustomer", new { id = customer.Id }, customer);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id,Customer customer)
        {
            if (customer == null || customer.Id != id)
            {
                return BadRequest();
            }

            var updatedCustomer = _ctx.customers.FirstOrDefault(c => c.Id == id);

            if (updatedCustomer == null)
            {
                return NotFound();
            }

            updatedCustomer.Email = customer.Email;
            updatedCustomer.Name = customer.Name;
            updatedCustomer.State = customer.State;

            _ctx.SaveChanges();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customer = _ctx.customers.FirstOrDefault(t => t.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            _ctx.customers.Remove(customer);
            _ctx.SaveChanges();
            return new NoContentResult();
        }
    }
}
