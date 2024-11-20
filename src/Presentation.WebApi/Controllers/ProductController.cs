using Application.Product;
using Application.Product.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApi.Base;

namespace Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductUseCases _product;

        public ProductController(ProductUseCases product)
        {
            _product = product;
        }

        [HttpPost]
        [Route("create-product")]
        public async Task<IActionResult> Create([FromBody] ProductRequest request)
        {
            var product = await _product.AddProduct.Execute(request);
            if (product.IsSuccess)
            {
                return StatusCode(201, BaseResponse.Created(product.Value, "Product created successfully"));
            }

            return StatusCode((int)product.StatusCode, BaseResponse.Error(product.Error, product.StatusCode));
        }

        [HttpPut]
        [Route("add-package")]
        public async Task<IActionResult> AddPackage([FromBody] List<UpdateProductRequest> requests)
        {
            var products = await _product.AddPackageInProduct.Execute(requests);
            if (products.IsSuccess)
            {
                return StatusCode(200, BaseResponse.Ok(products.Value, "Package added successfully on products"));
            }
            
            return StatusCode((int)products.StatusCode, BaseResponse.Error(products.Error, products.StatusCode));
        }

        [HttpGet]
        [Route("get-product/{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            var product = await _product.GetProduct.Execute(id);
            if (product.IsSuccess)
            {
                return StatusCode(200, BaseResponse.Ok(product.Value, "Product retrieved successfully"));
            }
            
            return StatusCode((int)product.StatusCode, BaseResponse.Error(product.Error, product.StatusCode));
        }
    }
}
