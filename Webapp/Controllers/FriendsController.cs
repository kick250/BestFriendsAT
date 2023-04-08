using Entities;
using Microsoft.AspNetCore.Mvc;
using Webapp.APIs;

namespace Webapp.Controllers;

public class FriendsController : Controller
{
    private FriendsAPI FriendsAPI { get; set; }
    private ImagesAPI ImagesAPI { get; set; }
    private StatesAPI StatesAPI { get; set; }

    public FriendsController(FriendsAPI friendsAPI, StatesAPI statesAPI, ImagesAPI imagesAPI)
    {
        FriendsAPI = friendsAPI;
        ImagesAPI = imagesAPI;
        StatesAPI = statesAPI;
    }

    public ActionResult Index()
    {
        List<Friend> friends = FriendsAPI.GetAll();

        return View(friends);
    }

    public ActionResult Details(int? id)
    {
        if (id == null) return RedirectToAction("Index");

        Friend friend = FriendsAPI.GetById((int)id);

        return View(friend);
    }

    public ActionResult New()
    {
        SetStates();
        return View();
    }

    [HttpPost]
    public ActionResult Create([FromForm] Friend friend, [FromForm] IFormFile friendImage)
    {
        try
        {
            string imageUrl = ImagesAPI.UploadImage(friendImage);
            friend.PhotoUrl = imageUrl;
            FriendsAPI.Create(friend);

            return RedirectToAction("Index");
        }
        catch (Exception ex) 
        {
            SetStates();
            ViewBag.Error = ex.Message; 
            return View("New", friend);
        }
    }

    public ActionResult Edit(int id)
    {
        Friend friend = FriendsAPI.GetById(id);
        SetStates();
        return View(friend);
    }

    [HttpPost]
    public ActionResult Update(Friend friend, [FromForm] IFormFile? friendImage)
    {
        try {
            if (friendImage != null)
            {
                string imageUrl = ImagesAPI.UploadImage(friendImage);
                friend.PhotoUrl = imageUrl;
            }
            FriendsAPI.Update(friend);
            return RedirectToAction("Details", new { Id = friend.Id });
        }
        catch (Exception ex)
        {
            SetStates();
            ViewBag.Error = ex.Message;
            return View("Edit", friend);
        }
    }

    public ActionResult AddFriend(int id)
    {
        List<Friend> allFriends = FriendsAPI.GetAll();

        allFriends = allFriends.Where(x => 
            x.Id != id && !x.IsFriendOf(id)
        ).ToList();

        ViewBag.Id = id;
        ViewBag.Friends = allFriends;

        return View();
    }

    public ActionResult RemoveFriend(int id)
    {
        Friend friend = FriendsAPI.GetById(id);

        ViewBag.Id = id;
        ViewBag.Friends = friend.Friends;

        return View();
    }

    [HttpPost]
    public ActionResult UpdateFriends(int id, int friendId, string action)
    {
        if (action == "add")
            FriendsAPI.AddFriendship(id, friendId);
        else if (action == "remove")
            FriendsAPI.RemoveFriendship(id, friendId);
        else
            return RedirectToAction("Index");

        return RedirectToAction("Details", new { Id = id });
    }

    public ActionResult Delete(int id)
    {
        Friend friend = FriendsAPI.GetById(id);
        return View(friend);
    }

    [HttpPost]
    public ActionResult Destroy(int id)
    {
        FriendsAPI.DeleteById(id);

        return RedirectToAction("Index");
    }

    #region private 
    private void SetStates()
    {
        List<State> states = StatesAPI.GetAll();
        ViewBag.States = states;
    }
    #endregion
}
