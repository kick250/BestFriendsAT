using BusinessServices;
using Microsoft.AspNetCore.Mvc;

namespace Webapi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FriendshipsController : ControllerBase
{
    FriendsService FriendsService { get; set; }

    public FriendshipsController(FriendsService friendsService) 
    {
        FriendsService = friendsService;
    }

    [HttpPost("{id}")]
    public IActionResult Create(int id, [FromQuery] int friendId)
    {
        FriendsService.AddFriendship(id, friendId);

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id, [FromQuery] int friendId)
    {
        FriendsService.RemoveFriendship(id, friendId);

        return Ok();
    }
}
