using Microsoft.AspNetCore.Mvc;
using BusinessServices;
using Infrastructure.Exceptions;

namespace Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        CountriesService CountriesService { get; set; }

        public CountriesController(CountriesService countriesServices) 
        {
            CountriesService = countriesServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(CountriesService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(CountriesService.GetById(id));
            } catch (CountryNotFoundException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            CountriesService.DeleteById(id);

            return Ok();
        }
    }
}
