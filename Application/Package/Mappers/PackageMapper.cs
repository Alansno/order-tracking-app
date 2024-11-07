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

    public PackageResponse ToDto(PackageResponse response)
    {
        return new PackageResponse
        {
            Id = response.Id,
            Code = response.Code,
            Destination = response.Destination
        };
    }

    public List<PackageResponse> ToDtoList(List<PackageResponse> packages)
    {
        return packages.Select(ToDto).ToList();
    }
}