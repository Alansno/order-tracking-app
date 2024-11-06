using Application.DeliveryMan;
using Application.DeliveryMan.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApp.Controllers;

public class DeliveryManController : Controller
{
    private readonly DeliveryManUseCases _deliveryManService;

    public DeliveryManController(DeliveryManUseCases deliveryManService)
    {
        _deliveryManService = deliveryManService;
    }
    // GET
    public IActionResult AddDeliveryMan()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Store([Bind("NameDelivery, NumPackages")] DeliveryManRequest deliveryManRequest)
    {
        if (ModelState.IsValid)
        {
            await _deliveryManService.addDeliveryMan.Execute(deliveryManRequest);
            ViewData["Message"] = "Repartidor agregado correctamente";
            return View("AddDeliveryMan");
        }

        return View("AddDeliveryMan");
    }
}