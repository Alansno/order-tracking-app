using Application.Shipping;
using Application.Shipping.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApi.Base;

namespace Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingController : ControllerBase
    {
        private readonly ShippingUseCases _shipping;

        public ShippingController(ShippingUseCases shipping)
        {
            _shipping = shipping;
        }

        [HttpPost]
        [Route("create-shipping")]
        public async Task<IActionResult> Create([FromBody] ShippingRequest request)
        {
            var shipping = await _shipping.AddShipping.Execute(request);
            if (shipping.IsSuccess)
            {
                return StatusCode(201, BaseResponse.Created(shipping.Value, "Shipping created successfully"));
            }

            return StatusCode((int)shipping.StatusCode, BaseResponse.Error(shipping.Error, shipping.StatusCode));
        }
    }
}
