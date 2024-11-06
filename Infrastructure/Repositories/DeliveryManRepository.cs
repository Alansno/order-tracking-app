using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Custom.ResultPattern;
using Infrastructure.Repositories.IRepositories;

namespace Infrastructure.Repositories;

public class DeliveryManRepository : IRepository<DeliveryManEntity>
{
    private readonly OrderTrackingContext _context;
    public DeliveryManRepository(OrderTrackingContext context)
    {
        _context = context;
    }
    public async Task<Result<DeliveryManEntity>> Save(DeliveryManEntity model)
    {
        if (model == null)
            return Result<DeliveryManEntity>.Failure("Model was found", System.Net.HttpStatusCode.NotFound);

        await _context.DeliveriesMan.AddAsync(model);
        await _context.SaveChangesAsync();
        return Result<DeliveryManEntity>.Success(model);
    }

    public async Task<Result<bool>> Update(DeliveryManEntity model)
    {
        if (model == null)
            return Result<bool>.Failure("model is empty", System.Net.HttpStatusCode.NoContent);

        var existingDeliveryMan = await _context.DeliveriesMan.FindAsync(model.Id);
        if (existingDeliveryMan == null)
            return Result<bool>.Failure("Delivery man was not found", System.Net.HttpStatusCode.NotFound);

        _context.DeliveriesMan.Update(model);
        await _context.SaveChangesAsync();
        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> Delete(int id)
    {
        var deliveryMan = await _context.DeliveriesMan.FindAsync(id);
        if (deliveryMan == null)
            return Result<bool>.Failure("Delivery man not found", System.Net.HttpStatusCode.NotFound);

        _context.DeliveriesMan.Remove(deliveryMan);
        await _context.SaveChangesAsync();
        return Result<bool>.Success(true);
    }

    public Task<Result<bool>> SoftDelete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<DeliveryManEntity>> FindById(int id)
    {
        var deliveryMan = await _context.DeliveriesMan.FindAsync(id);
        if (deliveryMan == null)
            return Result<DeliveryManEntity>.Failure("Delivery man not found", System.Net.HttpStatusCode.NotFound);

        return Result<DeliveryManEntity>.Success(deliveryMan);
    }

    public IQueryable<DeliveryManEntity> GetAll()
    {
        return _context.DeliveriesMan;
    }
}