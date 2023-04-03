using BusinessServices;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace Webapi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatesController : ControllerBase
{
    private StatesService StatesService { get; set; }

    public StatesController(StatesService statesService)
    {
        StatesService = statesService;
    }

    public IActionResult Index()
    {
        List<State> states = StatesService.GetAll();

        return Ok(states);
    }

    [HttpGet("{id}")]
    public IActionResult Show(int id)
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
