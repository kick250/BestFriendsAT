using Entities;
using Microsoft.AspNetCore.Mvc;
using Webapp.APIs;

namespace Webapp.Controllers;

public class FriendsController : Controller
{
    private FriendsAPI FriendsAPI { get; set; }

    public FriendsController(FriendsAPI friendsAPI)
    {
        FriendsAPI = friendsAPI;
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
        return View();
    }

    [HttpPost]
    public ActionResult Create()
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

    public ActionResult Edit(int id)
    {
        return View();
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
}
