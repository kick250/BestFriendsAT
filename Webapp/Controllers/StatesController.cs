using Entities;
using Microsoft.AspNetCore.Mvc;
using Webapp.APIs;

namespace Webapp.Controllers;

public class StatesController : Controller
{
    private StatesAPI StatesAPI { get; set; }
    private CountriesAPI CountriesAPI { get; set; }
    private ImagesAPI ImagesAPI { get; set; }

    public StatesController(StatesAPI statesAPI, ImagesAPI imagesAPI, CountriesAPI countriesAPI)
    {
        StatesAPI = statesAPI;
        CountriesAPI = countriesAPI;
        ImagesAPI = imagesAPI;
    }

    public ActionResult Index()
    {
        List<State> countries = StatesAPI.GetAll();

        return View(countries);
    }

    public ActionResult Details(int? id)
    {
        if (id == null) return RedirectToAction("Index");

        State state = StatesAPI.GetById((int)id);

        return View(state);
    }

    public ActionResult New()
    {
        ViewBag.countries = CountriesAPI.GetAll(); 
        return View();
    }

    [HttpPost]
    public ActionResult Create([FromForm] State state, IFormFile flagImage)
    {
        if (!ModelState.IsValid) return View("New", state);

        try
        {
            string flagUrl = ImagesAPI.UploadImage(flagImage);
            state.FlagUrl = flagUrl;

            StatesAPI.Create(state);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View("New", state);
        }
    }

    public ActionResult Edit(int id)
    {
        ViewBag.countries = CountriesAPI.GetAll(); 
        State state = StatesAPI.GetById(id);

        return View(state);
    }

    [HttpPost]
    public ActionResult Update([FromForm] State state, IFormFile? flagImage)
    {
        if (!ModelState.IsValid) return View("Edit", state);

        try
        {
            if (flagImage != null)
            {
                string flagUrl = ImagesAPI.UploadImage(flagImage);
                state.FlagUrl = flagUrl;
            }

            StatesAPI.Update(state);

            return RedirectToAction("Details", new { id = state.Id });
        }
        catch (Exception ex)
        {
            ViewBag.Errors = ex.Message;
            return View("Edit", state);
        }
    }

    public ActionResult Delete(int id)
    {
        State state = StatesAPI.GetById(id);

        return View(state);
    }

    [HttpPost]
    public ActionResult Destroy(int id)
    {
        try
        {
            StatesAPI.DeleteById(id);
            return RedirectToAction("Index");
        }
        catch
        {
            return RedirectToAction("Index");
        }
    }
}
