using Entities;
using Microsoft.AspNetCore.Mvc;
using Webapp.APIs;

namespace Webapp.Controllers;

public class CountriesController : Controller
{
    private CountriesAPI CountriesAPI { get; set; }

    public CountriesController(CountriesAPI countriesAPI)
    {
        CountriesAPI = countriesAPI;
    }

    public ActionResult Index()
    {
        List<Country> countries = CountriesAPI.GetAll();

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
