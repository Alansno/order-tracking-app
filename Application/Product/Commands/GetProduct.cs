using Domain.Entities;
using Infrastructure.Custom.ResultPattern;
using Infrastructure.Repositories.IRepositories;

namespace Application.Product.Commands;

public class GetProduct
{
    private readonly IRepository<ProductEntity> _productRepository;

    public GetProduct(IRepository<ProductEntity> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<ProductEntity>> Execute(int productId)
    {
        var product = await _productRepository.FindById(productId);
        if (product.IsSuccess)
        {
            return Result<ProductEntity>.Success(product.Value);
        }

        return Result<ProductEntity>.Failure(product.Error, product.StatusCode);
    }
}