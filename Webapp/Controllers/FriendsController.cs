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

    public ActionResult Details(int? id) // devolver friends tbm
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
        return View(friend);
    }

    [HttpPost]
    public ActionResult Update(int id)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    public ActionResult Delete(int id)
    {
        return View();
    }

    [HttpPost]
    public ActionResult Destroy(int id)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    #region private 
    private void SetStates()
    {
        List<State> states = StatesAPI.GetAll();
        ViewBag.States = states;
    }
    #endregion
}
