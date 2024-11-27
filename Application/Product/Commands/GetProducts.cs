using System.Net;
using Application.Product.Dtos;
using Dapper;
using Domain.Entities;
using Infrastructure.Custom.ResultPattern;
using Infrastructure.Repositories.IRepositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Application.Product.Commands;

public class GetProducts
{
    private readonly IRepository<ProductEntity> _productRepository;
    private readonly IConfiguration _config;
    public GetProducts(IRepository<ProductEntity> productRepository, IConfiguration config)
    {
        _productRepository = productRepository;
        _config = config;
    }

    public async Task<Result<List<ProductResponse>>> Execute()
    {
        var products = await _productRepository.GetAll().Where(p => p.PackageId == null)
            .Select(p => new ProductResponse { Id = p.Id, NameProduct = p.NameProduct, Package = p.Package}).ToListAsync();

        if (products.Count == 0)
        {
            return Result<List<ProductResponse>>.Failure("No products found", HttpStatusCode.NotFound);
        }

        return Result<List<ProductResponse>>.Success(products);
    }
}