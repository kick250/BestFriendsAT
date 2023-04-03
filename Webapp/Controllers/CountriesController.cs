using Entities;
using Microsoft.AspNetCore.Mvc;
using Webapp.APIs;

namespace Webapp.Controllers;

public class CountriesController : Controller
{
    private CountriesAPI CountriesAPI { get; set; }
    private ImagesAPI ImagesAPI { get; set; }

    public CountriesController(CountriesAPI countriesAPI, ImagesAPI imagesAPI)
    {
        CountriesAPI = countriesAPI;
        ImagesAPI = imagesAPI;
    }

    public ActionResult Index()
    {
        List<Country> countries = CountriesAPI.GetAll();

        return View(countries);
    }

    public ActionResult Details(int id)
    {
        try
        {
            Country country = CountriesAPI.GetById(id);
            return View(country);
        } catch (Exception)
        {
            return RedirectToAction("Index");
        }
    }

    public ActionResult New()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create([FromForm] Country country, [FromForm] IFormFile flagImage)
    {
        if (!ModelState.IsValid) return View("New", country);

        try
        {
            string flagUrl = ImagesAPI.UploadImage(flagImage);

            country.FlagUrl = flagUrl;

            CountriesAPI.Create(country);

            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            ViewBag.Error = e.Message;
            return View("New", country);
        }
    }

    public ActionResult Edit(int id)
    {
        try
        {
            Country country = CountriesAPI.GetById(id);
            return View(country);
        }
        catch (Exception)
        {
            return RedirectToAction("Index");
        }
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
        try
        {
            Country country = CountriesAPI.GetById(id);
            return View(country);
        }
        catch (Exception)
        {
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public ActionResult Destroy(int id)
    {
        try
        {
            CountriesAPI.DeleteById(id);

            return RedirectToAction("Index");
        }
        catch (Exception e) 
        {
            ViewBag.Error = e.Message;
            return View();
        }
    }
}
