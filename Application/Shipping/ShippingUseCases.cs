using Application.Shipping.Commands;
using Application.Shipping.Services;

namespace Application.Shipping;

public record class ShippingUseCases(
    AddShipping AddShipping,
    ShipmentDelivered ShipmentDelivered
    );