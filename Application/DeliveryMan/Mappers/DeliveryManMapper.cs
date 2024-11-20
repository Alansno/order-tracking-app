using Application.DeliveryMan.Dtos;
using Domain.Entities;

namespace Application.DeliveryMan.Mappers;

public class DeliveryManMapper
{
    public DeliveryManEntity ToEntity(DeliveryManRequest request)
    {
        return new DeliveryManEntity
        {
            NameDelivery = request.NameDelivery,
            Availability = true,
            NumPackages = request.NumPackages,
        };
    }

    public DeliveryManResponse ToDto(DeliveryManEntity entity)
    {
        return new DeliveryManResponse
        {
            Id = entity.Id,
            NameDelivery = entity.NameDelivery,
            Availability = entity.Availability,
            NumPackages = entity.NumPackages,
        };
    }

    public List<DeliveryManResponse> ToDtoList(List<DeliveryManEntity> entities)
    {
        return entities.Select(ToDto).ToList();
    }
}