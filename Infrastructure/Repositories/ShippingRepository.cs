using System.Linq.Expressions;
using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Custom.ResultPattern;
using Infrastructure.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ShippingRepository : IRepository<ShippingEntity>, ISearchRepository<ShippingEntity>, IShippingRepository
{
    private readonly OrderTrackingContext _context;

    public ShippingRepository(OrderTrackingContext context)
    {
        _context = context;
    }
    public async Task<Result<ShippingEntity>> Save(ShippingEntity model)
    {
        if (model == null)
            return Result<ShippingEntity>.Failure("Model was found", System.Net.HttpStatusCode.NotFound);

        await _context.Shippings.AddAsync(model);
        await _context.SaveChangesAsync();
        return Result<ShippingEntity>.Success(model);
    }

    public Task<Result<bool>> Update(ShippingEntity model)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> SoftDelete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<ShippingEntity>> FindById(int id)
    {
        var shipping = await _context.Shippings.FindAsync(id);
        if (shipping == null)
            return Result<ShippingEntity>.Failure("Shipping not found", System.Net.HttpStatusCode.NotFound);

        return Result<ShippingEntity>.Success(shipping);
    }

    public IQueryable<ShippingEntity> GetAll()
    {
        return _context.Shippings;
    }

    public async Task<Result<IQueryable<ShippingEntity>>> FilteredSearch(Expression<Func<ShippingEntity, bool>> predicate)
    {
        var shippings = _context.Shippings.Where(predicate);
        if (!await shippings.AnyAsync())
        {
            return Result<IQueryable<ShippingEntity>>.Failure("No shippings found", System.Net.HttpStatusCode.NotFound);
        }

        return Result<IQueryable<ShippingEntity>>.Success(shippings);
    }

    public async Task<Result<ShippingEntity>> CreateShipment(ShippingEntity shippingEntity, int deliveryManId)
    {
        shippingEntity.DeliveryManId = deliveryManId;
        shippingEntity.Status = "Asignado";
        shippingEntity.ShippingDate = DateTime.Now;
        shippingEntity.UpdatedAt = DateTime.Now;
        
        _context.Entry(shippingEntity).Property(s => s.DeliveryManId).IsModified = true;
        _context.Entry(shippingEntity).Property(s => s.Status).IsModified = true;
        _context.Entry(shippingEntity).Property(s => s.ShippingDate).IsModified = true;
        _context.Entry(shippingEntity).Property(s => s.UpdatedAt).IsModified = true;
        await _context.SaveChangesAsync();
        return Result<ShippingEntity>.Success(shippingEntity);
    }

    public async Task<Result<ShippingEntity>> ShipmentDeliveredUpdated(ShippingEntity shippingEntity)
    {
        shippingEntity.Status = "Entregado";
        shippingEntity.DeliveryDate = DateTime.Now;
        _context.Entry(shippingEntity).Property(s => s.Status).IsModified = true;
        _context.Entry(shippingEntity).Property(s => s.DeliveryDate).IsModified = true;
        await _context.SaveChangesAsync();
        return Result<ShippingEntity>.Success(shippingEntity);
    }
}