using BusinessServices;
using Microsoft.AspNetCore.Mvc;


namespace Webapi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FriendsController : ControllerBase
{
    private FriendsService FriendsService { get; set; }

    public FriendsController(FriendsService friendsService)
    {
        FriendsService = friendsService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return Ok(FriendsService.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult Show(int id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public IActionResult Create([FromBody] string value)
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
