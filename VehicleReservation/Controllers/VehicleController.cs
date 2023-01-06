﻿using Microsoft.AspNetCore.Mvc;

namespace VehicleReservation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        // GET: api/<VehicleReservationController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<VehicleReservationController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<VehicleReservationController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<VehicleReservationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VehicleReservationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
