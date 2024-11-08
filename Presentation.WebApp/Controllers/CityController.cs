using Application.City;
using Application.City.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApp.Controllers;

public class CityController : Controller
{
    private readonly CityUseCases _city;

    public CityController(CityUseCases city)
    {
        _city = city;
    }
    
    // GET
    public IActionResult AddCity()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Store([Bind("CityName")] CityRequest request)
    {
        if (ModelState.IsValid)
        {
            await _city.AddCity.Execute(request);
            ViewData["Message"] = "Ciudad agregada correctamente";
            return View("AddCity");
        }

        return View("AddCity");
    }
}