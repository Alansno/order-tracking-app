using System.Net;
using Application.Product.Dtos;
using Application.Product.Mappers;
using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Custom.ResultPattern;
using Infrastructure.Repositories.IRepositories;

namespace Application.Product.Commands;

public class AddPackageInProduct
{
    private readonly IProductRepository _productRepository;
    private readonly OrderTrackingContext _context;
    private readonly ProductMapper _productMapper;

    public AddPackageInProduct(IProductRepository productRepository, ProductMapper productMapper, OrderTrackingContext context)
    {
        _productRepository = productRepository;
        _productMapper = productMapper;
        _context = context;
    }

    public async Task<Result<List<ProductResponse>>> Execute(List<UpdateProductRequest> products)
    {
        var productsUpdated = new List<ProductEntity>();
        foreach (var product in products)
        {
            var res  = await _productRepository.UpdateProduct(product.PackageId, product.ProductId);
            if (!res.IsSuccess)
            {
                return Result<List<ProductResponse>>.Failure(res.Error, res.StatusCode);
            }
            
            productsUpdated.Add(res.Value);
            await _context.SaveChangesAsync();
        }
        
        return Result<List<ProductResponse>>.Success(_productMapper.ToDtoList(productsUpdated));
    }
}