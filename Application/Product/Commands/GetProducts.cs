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

    public async Task<Result<IEnumerable<ProductResponse>>> Execute()
    {
        var connectionString = _config.GetConnectionString("Connection");
        await using var connection = new SqlConnection(connectionString);

        var sql = @"SELECT p.Id, p.NameProduct FROM Products p WHERE PackageId IS NULL";
        var products = await connection.QueryAsync<ProductResponse>(sql);
        Console.WriteLine(products.ToString());
        if (products.Count() != 0)
        {
            return Result<IEnumerable<ProductResponse>>.Success(products.ToList());
        }
        
        return Result<IEnumerable<ProductResponse>>.Failure("No products found", HttpStatusCode.NotFound);
    }
}