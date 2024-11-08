using System.Net;
using Application.Product.Dtos;
using Domain.Entities;
using Infrastructure.Custom.ResultPattern;
using Infrastructure.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Application.Product.Commands;

public class GetProducts
{
    private readonly IRepository<ProductEntity> _productRepository;

    public GetProducts(IRepository<ProductEntity> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<IEnumerable<ProductResponse>>> Execute()
    {
        var products = await _productRepository.GetAll()
            .Where(p => p.PackageId == null)
            .Select(a => new ProductResponse{NameProduct = a.NameProduct, Id = a.Id}).ToListAsync();
        

        if (products.Count != 0)
        {
            return Result<IEnumerable<ProductResponse>>.Success(products);
        }
        
        return Result<IEnumerable<ProductResponse>>.Failure("No products found", HttpStatusCode.NotFound);
    }
}