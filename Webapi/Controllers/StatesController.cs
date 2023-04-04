using BusinessServices;
using Entities;
using Infrastructure.Exceptions;
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
        try
        {
            State state = StatesService.GetById(id);
            return Ok(state);
        } catch (StateNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public IActionResult Post([FromBody] State state)
    {
        StatesService.Create(state);

        return Ok(state);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] string value)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        StatesService.DeleteById(id);
        return NoContent();
    }
}
