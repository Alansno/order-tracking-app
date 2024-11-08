using Application.Product.Dtos;
using Domain.Entities;

namespace Application.Product.Mappers;

public class ProductMapper
{
    public ProductEntity ToEntity(ProductRequest request)
    {
        return new ProductEntity
        {
            NameProduct = request.NameProduct,
            DescriptionProduct = request.DescriptionProduct,
            PackageId = request.PackageId,
        };
    }

    public ProductResponse ToDto(ProductEntity productEntity)
    {
        return new ProductResponse
        {
            Id = productEntity.Id,
            NameProduct = productEntity.NameProduct,
        };
    }

}