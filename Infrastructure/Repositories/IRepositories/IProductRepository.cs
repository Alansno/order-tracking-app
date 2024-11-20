using Domain.Entities;
using Infrastructure.Custom.ResultPattern;

namespace Infrastructure.Repositories.IRepositories;

public interface IProductRepository
{
    Task<Result<ProductEntity>> UpdateProduct(int packageId, int productId);
}