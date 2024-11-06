using System.Data;
using System.Net;
using Application.Package;
using Application.Package.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApp.Controllers;

public class PackageController : Controller
{
    private readonly PackageUseCases _package;

    public PackageController(PackageUseCases package)
    {
        _package = package;
    }
    
    // GET
    public IActionResult AddPackage()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Store([Bind("Code")] PackageRequest request)
    {
        if (ModelState.IsValid)
        {
            var package = await _package.AddPackage.Execute(request);
            if (package.IsSuccess)
            {
                ViewData["Message"] = "Paquete agregado correctamente";
                return View("AddPackage");    
            }

            if(package.StatusCode == HttpStatusCode.Conflict)
            {
                ViewData["ErrorMessage"] = "El codigo del paquete ya está en uso";
                return View("AddPackage");
            }

        }

        return View("AddPackage");
    }
}