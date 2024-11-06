using Application.Package.Dtos;
using Domain.Entities;

namespace Application.Package.Mappers;

public class PackageMapper
{
    public PackageEntity ToEntity(PackageRequest request)
    {
        return new PackageEntity
        {
            Code = request.Code,
            ShippingId = request.ShippingId,
        };
    }
}