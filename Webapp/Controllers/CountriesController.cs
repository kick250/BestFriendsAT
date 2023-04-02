using Entities;
using Microsoft.AspNetCore.Mvc;

namespace Webapp.Controllers;

public class CountriesController : Controller
{
    public ActionResult Index()
    {
        var countries = new List<Country>();

        return View(countries);
    }

    public ActionResult Details(int id)
    {
        return View();
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
