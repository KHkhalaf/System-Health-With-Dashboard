using Advantage.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Advantage.API.Controllers
{
    [Route("api/[controller]")]
    public class DeliveryController : Controller
    {
        private readonly ApiContext _ctx;

        public DeliveryController(ApiContext ctx)
        {
            _ctx = ctx;
        }

        // GET api/delivery/pageNumber/pageSize
        [HttpGet("{pageIndex:int}/{pageSize:int}")]
        public IActionResult Get(int pageIndex, int pageSize)
        {
            var data = _ctx.deliveries.OrderBy(d => d.Id);
            var page = new PaginatedResponse<Delivery>(data, pageIndex, pageSize);

            return Ok(page);
        }

        // GET api/delivery/available
        [HttpGet("available")]
        public IActionResult GetAvailable()
        {
            var availableDeliveries = _ctx.deliveries
                .Where(d => d.IsAvailable)
                .ToList();

            return Ok(availableDeliveries);
        }

        // GET api/delivery/5
        [HttpGet("{id}", Name = "GetDelivery")]
        public IActionResult Get(int id)
        {
            var delivery = _ctx.deliveries.Find(id);
            if (delivery == null)
            {
                return NotFound();
            }

            return Ok(delivery);
        }

        // POST api/delivery
        [HttpPost]
        public IActionResult Post(Delivery delivery)
        {
            if (delivery == null)
            {
                return BadRequest();
            }

            _ctx.deliveries.Add(delivery);
            _ctx.SaveChanges();

            return CreatedAtRoute("GetDelivery", new { id = delivery.Id }, delivery);
        }

        // PUT api/delivery/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Delivery delivery)
        {
            if (delivery == null || delivery.Id != id)
            {
                return BadRequest();
            }

            var updatedDelivery = _ctx.deliveries.FirstOrDefault(d => d.Id == id);

            if (updatedDelivery == null)
            {
                return NotFound();
            }

            updatedDelivery.Name = delivery.Name;
            updatedDelivery.Phone = delivery.Phone;
            updatedDelivery.Vehicle = delivery.Vehicle;
            updatedDelivery.LicensePlate = delivery.LicensePlate;
            updatedDelivery.IsAvailable = delivery.IsAvailable;
            updatedDelivery.AssignedDate = delivery.AssignedDate;

            _ctx.SaveChanges();
            return new NoContentResult();
        }

        // DELETE api/delivery/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var delivery = _ctx.deliveries.FirstOrDefault(d => d.Id == id);
            if (delivery == null)
            {
                return NotFound();
            }

            // Check if delivery is assigned to any orders
            var ordersWithDelivery = _ctx.orders.Any(o => o.DeliveryId == id);
            if (ordersWithDelivery)
            {
                return BadRequest("Cannot delete delivery that is assigned to orders");
            }

            _ctx.deliveries.Remove(delivery);
            _ctx.SaveChanges();
            return new NoContentResult();
        }

        // PUT api/delivery/5/assign
        [HttpPut("{id}/assign")]
        public IActionResult AssignDelivery(int id)
        {
            var delivery = _ctx.deliveries.FirstOrDefault(d => d.Id == id);
            if (delivery == null)
            {
                return NotFound();
            }

            if (!delivery.IsAvailable)
            {
                return BadRequest("Delivery is not available");
            }

            delivery.IsAvailable = false;
            delivery.AssignedDate = DateTime.Now;

            _ctx.SaveChanges();
            return Ok(delivery);
        }

        // PUT api/delivery/5/release
        [HttpPut("{id}/release")]
        public IActionResult ReleaseDelivery(int id)
        {
            var delivery = _ctx.deliveries.FirstOrDefault(d => d.Id == id);
            if (delivery == null)
            {
                return NotFound();
            }

            delivery.IsAvailable = true;
            delivery.AssignedDate = null;

            _ctx.SaveChanges();
            return Ok(delivery);
        }
    }
}

