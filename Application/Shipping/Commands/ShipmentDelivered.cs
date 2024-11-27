using System.Net;
using Application.Shipping.Dtos;
using Application.Shipping.Mappers;
using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Custom.ResultPattern;
using Infrastructure.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Application.Shipping.Commands;

public class ShipmentDelivered
{
    private readonly IRepository<ShippingEntity> _shippingRepository;
    private readonly IShippingRepository _shippingCustomRepository;
    private readonly IDeliveryManRepository _deliveryManRepository;
    private readonly IRepository<DeliveryManEntity> _deliveryManCustomRepository;
    private readonly ShippingMapper _shippingMapper;
    private readonly OrderTrackingContext _context;

    public ShipmentDelivered(IRepository<ShippingEntity> shippingRepository, IShippingRepository shippingCustomRepository, IDeliveryManRepository deliveryManRepository, ShippingMapper shippingMapper, OrderTrackingContext context, IRepository<DeliveryManEntity> deliveryManCustomRepository)
    {
        _shippingRepository = shippingRepository;
        _shippingCustomRepository = shippingCustomRepository;
        _deliveryManRepository = deliveryManRepository;
        _shippingMapper = shippingMapper;
        _context = context;
        _deliveryManCustomRepository = deliveryManCustomRepository;
    }

    public async Task<Result<ShippingResponse>> Execute(int shippingId)
    {
        var shipping = await _shippingRepository.GetAll().Where(s => s.Id == shippingId && s.Status == "Asignado").FirstOrDefaultAsync();
        if (shipping is null)
        {
            return Result<ShippingResponse>.Failure("El envío no existe o ya fue entregado", HttpStatusCode.Conflict);
        }
        
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var shipmentDelivered = await _shippingCustomRepository.ShipmentDeliveredUpdated(shipping);
            
            var deliveryMan = await _deliveryManCustomRepository.FindById(shipmentDelivered.Value.DeliveryManId ?? 0);
            if (!deliveryMan.IsSuccess)
            {
                await transaction.RollbackAsync();
                return Result<ShippingResponse>.Failure(deliveryMan.Error, deliveryMan.StatusCode);
            }

            await _deliveryManRepository.ChangeAvailability(deliveryMan.Value, true);
            await transaction.CommitAsync();

            return Result<ShippingResponse>.Success(_shippingMapper.ToDto(shipmentDelivered.Value));
        }
        catch (DbUpdateConcurrencyException e)
        {
            await transaction.RollbackAsync();
            return Result<ShippingResponse>.Failure($"{e.Message}", HttpStatusCode.InternalServerError);
        }
    }
}