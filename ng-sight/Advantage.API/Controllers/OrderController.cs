using Advantage.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Advantage.API.Demo.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly ApiContext _ctx;

        public OrderController(ApiContext ctx)
        {
            _ctx = ctx;
        }

        // GET api/order/pageNumber/pageSize
        [HttpGet("{pageIndex:int}/{pageSize:int}")]
        public IActionResult Get(int pageIndex, int pageSize)
        {
            var data = _ctx.orders
                .Include(o => o.customer)
                .Include(o => o.delivery)
                .OrderByDescending(c => c.Placed);
            var page = new PaginatedResponse<Order>(data, pageIndex, pageSize);

            return Ok(page);
        }

        [HttpGet("bystate")]
        public IActionResult ByState()
        {
            // enumerated group due to ef error
            // https://github.com/aspnet/EntityFrameworkCore/issues/9551

            var orders = _ctx.orders
                .Include(o => o.customer)
                .Include(o => o.delivery)
                .ToList();
            var groupedResult = orders
                .GroupBy(r => r.customer.State)
                .ToList()
                .Select(grp => new 
                {
                    State = grp.Key,
                    Total = grp.Sum(x => x.OrderTotal)
                }).OrderByDescending(r => r.Total)
                .ToList();

            return Ok(groupedResult);
        }

        [HttpGet("bycustomer/{n}")]
        public IActionResult ByCustomer(int n)
        {
            // enumerated group due to ef error
            // https://github.com/aspnet/EntityFrameworkCore/issues/9551

            var orders = _ctx.orders
                .Include(o => o.customer)
                .Include(o => o.delivery)
                .ToList();
            var groupedResult = orders
                .GroupBy(r => r.customer.Id)
                .ToList()
                .Select(grp => new
                {
                    Name = _ctx.customers.Find(grp.Key).Name,
                    Total = grp.Sum(x => x.OrderTotal)
                }).OrderByDescending(r => r.Total)
                .Take(n)
                .ToList();

            return Ok(groupedResult);
        }

        // GET api/order/5
        [HttpGet("getorder/{id}", Name ="GetOrder")]
        public IActionResult GetOrder(int id)
        {
            var order = _ctx.orders
                .Include(o => o.customer)
                .Include(o => o.delivery)
                .FirstOrDefault(o => o.Id == id);
            
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // POST api/order
        [HttpPost]
        public IActionResult Post(Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }

            _ctx.orders.Add(order);
            _ctx.SaveChanges();

            return CreatedAtRoute("GetOrder", new { id = order.Id }, order);
        }

        // PUT api/order/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Order order)
        {
            if (order == null || order.Id != id)
            {
                return BadRequest();
            }

            var updatedOrder = _ctx.orders.FirstOrDefault(c => c.Id == id);

            if (updatedOrder == null)
            {
                return NotFound();
            }

            updatedOrder.customer = order.customer;
            updatedOrder.delivery = order.delivery;
            updatedOrder.DeliveryId = order.DeliveryId;
            updatedOrder.Completed = order.Completed;
            updatedOrder.OrderTotal = order.OrderTotal;
            updatedOrder.Placed = order.Placed;

            _ctx.SaveChanges();
            return new NoContentResult();
        }

        // DELETE api/order/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var order = _ctx.orders.FirstOrDefault(t => t.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            _ctx.orders.Remove(order);
            _ctx.SaveChanges();
            return new NoContentResult();
        }

        // PUT api/order/5/assigndelivery/{deliveryId}
        [HttpPut("{id}/assigndelivery/{deliveryId}")]
        public IActionResult AssignDelivery(int id, int deliveryId)
        {
            var order = _ctx.orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound("Order not found");
            }

            var delivery = _ctx.deliveries.FirstOrDefault(d => d.Id == deliveryId);
            if (delivery == null)
            {
                return NotFound("Delivery not found");
            }

            if (!delivery.IsAvailable)
            {
                return BadRequest("Delivery is not available");
            }

            order.DeliveryId = deliveryId;
            order.delivery = delivery;
            delivery.IsAvailable = false;
            delivery.AssignedDate = DateTime.Now;

            _ctx.SaveChanges();
            return Ok(order);
        }

        // PUT api/order/5/releasedelivery
        [HttpPut("{id}/releasedelivery")]
        public IActionResult ReleaseDelivery(int id)
        {
            var order = _ctx.orders
                .Include(o => o.delivery)
                .FirstOrDefault(o => o.Id == id);
            
            if (order == null)
            {
                return NotFound("Order not found");
            }

            if (order.delivery == null)
            {
                return BadRequest("Order has no delivery assigned");
            }

            var delivery = order.delivery;
            order.DeliveryId = null;
            order.delivery = null;
            delivery.IsAvailable = true;
            delivery.AssignedDate = null;

            _ctx.SaveChanges();
            return Ok(order);
        }

        // GET api/order/bydelivery/{deliveryId}
        [HttpGet("bydelivery/{deliveryId}")]
        public IActionResult ByDelivery(int deliveryId)
        {
            var orders = _ctx.orders
                .Include(o => o.customer)
                .Include(o => o.delivery)
                .Where(o => o.DeliveryId == deliveryId)
                .OrderByDescending(o => o.Placed)
                .ToList();

            return Ok(orders);
        }
    }
}