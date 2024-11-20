using Application.Product.Dtos;
using Application.Product.Mappers;
using Domain.Entities;
using Infrastructure.Custom.ResultPattern;
using Infrastructure.Repositories.IRepositories;

namespace Application.Product.Commands;

public class AddProduct
{
    private readonly IRepository<ProductEntity> _productRepository;
    private readonly ProductMapper _productMapper;

    public AddProduct(IRepository<ProductEntity> productRepository, ProductMapper productMapper)
    {
        _productRepository = productRepository;
        _productMapper = productMapper;
    }

    public async Task<Result<ProductResponse>> Execute(ProductRequest request)
    {
        var product = _productMapper.ToEntity(request);
        var productSaved = await _productRepository.Save(product);

        if (productSaved.IsSuccess)
        {
            return Result<ProductResponse>.Success(_productMapper.ToDto(productSaved.Value));
        }
        
        return Result<ProductResponse>.Failure(productSaved.Error, productSaved.StatusCode);
    }
}