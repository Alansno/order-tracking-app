using Application.Package;
using Application.Package.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApi.Base;

namespace Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly PackageUseCases _package;

        public PackageController(PackageUseCases package)
        {
            _package = package;
        }

        [HttpPost]
        [Route("create-package")]
        public async Task<IActionResult> CreatePackage([FromBody] PackageRequest request)
        {
            var package = await _package.AddPackage.Execute(request);
            if (package.IsSuccess)
            {
                return StatusCode(201, BaseResponse.Created(package.Value, "Package created successfully"));
            }

            return StatusCode((int)package.StatusCode, BaseResponse.Error(package.Error, package.StatusCode));
        }

        [HttpGet]
        [Route("get-packages")]
        public async Task<IActionResult> GetPackages()
        {
            var packages = await _package.GetPackages.Execute();
            if (packages.IsSuccess)
            {
                return StatusCode(200, BaseResponse.Ok(packages.Value));
            }
            
            return StatusCode((int)packages.StatusCode, BaseResponse.Error(packages.Error, packages.StatusCode));
        }
    }
}
