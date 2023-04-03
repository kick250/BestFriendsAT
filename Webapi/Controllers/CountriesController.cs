using Microsoft.AspNetCore.Mvc;
using BusinessServices;
using Infrastructure.Exceptions;
using Entities;

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
        public IActionResult Show(int id)
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
        public IActionResult Post([FromBody] Country country)
        {
            CountriesService.Create(country);

            return Created("", country);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Country country)
        {
            country.Id = id;
            CountriesService.Update(country);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            CountriesService.DeleteById(id);

            return Ok();
        }
    }
}
