using Application.Shipping.Dtos;
using Domain.Entities;

namespace Application.Shipping.Mappers;

public class ShippingMapper
{
    public ShippingEntity ToEntity(ShippingRequest request, int cityId)
    {
        return new ShippingEntity
        {
            Origin = request.Origin,
            CityId = cityId,

        };
    }

    public ShippingResponse ToDto(ShippingEntity shippingEntity)
    {
        return new ShippingResponse
        {
            Id = shippingEntity.Id,
            Origin = shippingEntity.Origin,
            Status = shippingEntity.Status,
            DeliveryDate = shippingEntity.DeliveryDate,
        };
    }
}