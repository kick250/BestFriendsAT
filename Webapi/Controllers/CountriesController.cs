using Microsoft.AspNetCore.Mvc;
using BusinessServices;

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
        public IActionResult Get()
        {
            return Ok(CountriesService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
