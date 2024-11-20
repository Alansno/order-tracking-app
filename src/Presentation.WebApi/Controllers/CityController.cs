using Application.City;
using Application.City.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApi.Base;

namespace Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly CityUseCases _city;

        public CityController(CityUseCases city)
        {
            _city = city;
        }

        [HttpPost]
        [Route("create-city")]
        public async Task<IActionResult> CreateCity([FromBody] CityRequest request)
        {
            var city = await _city.AddCity.Execute(request);
            if (city.IsSuccess)
            {
                return StatusCode(201, BaseResponse.Created(city.Value, "City created successfully"));
            }

            return StatusCode((int)city.StatusCode, BaseResponse.Error(city.Error, city.StatusCode));
        }

        [HttpGet]
        [Route("get-cities")]
        public async Task<IActionResult> GetCities()
        {
            var cities = await _city.GetCities.Execute();
            if (cities.IsSuccess)
            {
                return StatusCode(200, BaseResponse.Ok(cities.Value));
            }
            
            return StatusCode((int)cities.StatusCode, BaseResponse.Error(cities.Error, cities.StatusCode));
        }
    }
}
