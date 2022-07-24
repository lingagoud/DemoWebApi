using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FruitsController : ControllerBase
    {
        public static List<string> Fruits = new List<string> { "Apple", "Banana", "Orange", "Grapes", "Mango" };
        // GET: api/<FruitsController>
        [HttpGet]
        [Route("ListFruits")] 
        public IEnumerable<string> Get()
        {
            return Fruits;
        }

        // GET api/<FruitsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return Fruits[id];
        }

        // POST api/<FruitsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            Fruits.Add(value);
        }

        // PUT api/<FruitsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            Fruits[id] = value;
        }

        // DELETE api/<FruitsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Fruits.RemoveAt(id);
        }
    }
}
