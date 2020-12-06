using System;
using System.Collections.Generic;
using Ingos.AspNetCore.Core.Tests.Dtos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ingos.AspNetCore.Core.Tests.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SampleController : ControllerBase
    {
        // GET: api/<Sample>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        // GET api/<Sample>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            throw new Exception($"Exception handler Test, request info：{id}");
        }

        // POST api/<Sample>
        [HttpPost]
        public void Post([FromBody] SampleDto dto)
        {
        }

        // PUT api/<Sample>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Sample>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}