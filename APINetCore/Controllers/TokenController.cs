using APINetCore.Models;
using APINetCore.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APINetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TokenController(IConfiguration configuration) {
            _configuration = configuration;
        }


        // GET: api/<TokenController>
        [HttpGet]
        public IEnumerable<string> Get()           
        {                                    
            var jwtService = new JwtService(_configuration);
            var userId = "123"; // Cambia esto al ID del usuario autenticado
            var token = jwtService.GenerateToken(userId);
            return new string[] { token };
        }

        // GET api/<TokenController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TokenController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TokenController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TokenController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
