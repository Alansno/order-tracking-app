using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Custom.ResultPattern;
using Infrastructure.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository : IRepository<ProductEntity>
{
    private readonly OrderTrackingContext _context;

    public ProductRepository(OrderTrackingContext context)
    {
        _context = context;
    }
    
    public async Task<Result<ProductEntity>> Save(ProductEntity model)
    {
        try
        {
            if (model == null)
                return Result<ProductEntity>.Failure("Model was found", System.Net.HttpStatusCode.NotFound);

            await _context.Products.AddAsync(model);
            await _context.SaveChangesAsync();
            return Result<ProductEntity>.Success(model);
        }
        catch (DbUpdateException ex)
        {
            return Result<ProductEntity>.Failure("Something went wrong", System.Net.HttpStatusCode.InternalServerError);
        }
    }

    public Task<Result<bool>> Update(ProductEntity model)
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

    public Task<Result<ProductEntity>> FindById(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<ProductEntity> GetAll()
    {
        return _context.Products;
    }
}