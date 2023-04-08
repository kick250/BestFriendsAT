using BusinessServices;
using Entities;
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
        return Ok(FriendsService.GetById(id));
    }

    [HttpPost]
    public IActionResult Create([FromBody] Friend friend)
    {
        try
        {
            FriendsService.Create(friend);

            return Ok("Esse amigo foi criado");
        } catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] string value)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        FriendsService.DeleteById(id);
        return Ok();
    }
}
