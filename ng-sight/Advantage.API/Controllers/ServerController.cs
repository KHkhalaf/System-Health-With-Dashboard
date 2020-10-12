using Advantage.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Advantage.API.Controllers
{
    [Route("api/[controller]")]
    public class ServerController : Controller
    {
        private readonly ApiContext _ctx;

        public ServerController(ApiContext ctx)
        {
            _ctx = ctx;
        }

        // GET api/server
        [HttpGet]
        public IActionResult Get()
        {
            var response = _ctx.Servers.OrderBy(s=>s.Id).ToList(); 
            return Ok(response);
        }

        // GET api/server/5
        [HttpGet("{id}", Name ="GetServer")]
        public Server Get(int id)
        {
            return _ctx.Servers.Find(id);
        }

        // POST api/server
        [HttpPost]
        public IActionResult Post([FromBody] Server server)
        {
            if (server == null)
            {
                return BadRequest();
            }

            _ctx.Servers.Add(server);
            _ctx.SaveChanges();

            return CreatedAtRoute("GetServer", new { id = server.Id }, server);
        }

        [HttpPut("{id}")]
        public IActionResult Message(int id, string msg)
        {

            var server = _ctx.Servers.FirstOrDefault(s => s.Id == id);

            if (server == null)
            {
                return NotFound();
            }

            // move update handling to a service, perhaps
            if(msg == "activate")
            {
                server.isOnline = true;
                _ctx.SaveChanges();
            }

            if(msg == "deactivate")
            {
                server.isOnline = false;
                _ctx.SaveChanges();
            }

            return Ok(server.isOnline.ToString());
        }

        // DELETE api/server/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var server = _ctx.Servers.FirstOrDefault(t => t.Id == id);
            if (server == null)
            {
                return NotFound();
            }

            _ctx.Servers.Remove(server);
            _ctx.SaveChanges();
            return new NoContentResult();
        }
    }
}