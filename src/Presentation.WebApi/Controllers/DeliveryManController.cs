using Application.DeliveryMan;
using Application.DeliveryMan.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApi.Base;

namespace Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryManController : ControllerBase
    {
        private readonly DeliveryManUseCases _deliveryMan;

        public DeliveryManController(DeliveryManUseCases deliveryMan)
        {
            _deliveryMan = deliveryMan;
        }

        [HttpPost]
        [Route("createDeliveryMan")]
        public async Task<IActionResult> Create([FromBody] DeliveryManRequest request)
        {
            var deliveryMan = await _deliveryMan.addDeliveryMan.Execute(request);
            if (deliveryMan.IsSuccess)
            {
                return StatusCode(201, BaseResponse.Created(deliveryMan.Value, "Delivery man created successfully"));
            }
            
            return StatusCode((int)deliveryMan.StatusCode, BaseResponse.Error(deliveryMan.Error, deliveryMan.StatusCode));
        }

        [HttpGet]
        [Route("getDeliveryMan")]
        public async Task<IActionResult> GetDeliveryMan([FromQuery] string? nameDeliveryMan, [FromQuery] int? numPackages)
        {
            var deliveriesMan = await _deliveryMan.getDeliveryManWith.Execute(d => d.NameDelivery.Contains(nameDeliveryMan) || d.NumPackages == numPackages);
            if (deliveriesMan.IsSuccess)
            {
                return StatusCode(200, BaseResponse.Ok(deliveriesMan.Value));
            }
            
            return StatusCode((int)deliveriesMan.StatusCode, BaseResponse.Error(deliveriesMan.Error, deliveriesMan.StatusCode));
        }
    }
}
