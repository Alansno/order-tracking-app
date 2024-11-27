using Domain.Entities;
using Infrastructure.Custom.ResultPattern;

namespace Infrastructure.Repositories.IRepositories;

public interface IShippingRepository
{
    Task<Result<ShippingEntity>> CreateShipment(ShippingEntity shippingEntity, int deliveryManId);
    Task<Result<ShippingEntity>> ShipmentDeliveredUpdated(ShippingEntity shippingEntity);
}