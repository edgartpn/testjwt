using APINetCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APINetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerminalesController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly DbContextOptions<MembershipContext> _dbContextOptions;

        public TerminalesController(IConfiguration configuration, DbContextOptions<MembershipContext> dbContextOptions)
        {
            _configuration = configuration;
            _dbContextOptions = dbContextOptions;
        }
        // GET: api/<TerminalesController>
        [HttpGet]
        [Authorize]
        public IEnumerable<Imex_Terminal> Get()
        {
            IEnumerable<Imex_Terminal> resultado;

            using (var db = new MembershipContext(_dbContextOptions, _configuration)) {

                resultado = db.Imex_Terminal.Where(r=> r.TerminalId>=5).ToList().AsEnumerable();
            
            }           

            return resultado;
        }

        // GET api/<TerminalesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TerminalesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TerminalesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TerminalesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
