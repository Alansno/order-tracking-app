using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Custom.ResultPattern;
using Infrastructure.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CityRepository : IRepository<CityEntity>
{
    private readonly OrderTrackingContext _context;

    public CityRepository(OrderTrackingContext context)
    {
        _context = context;
    }
    
    public async Task<Result<CityEntity>> Save(CityEntity model)
    {
        try
        {
            if (model == null)
                return Result<CityEntity>.Failure("Model was found", System.Net.HttpStatusCode.NotFound);

            await _context.Cities.AddAsync(model);
            await _context.SaveChangesAsync();
            return Result<CityEntity>.Success(model);
        }
        catch (DbUpdateException ex)
        {
            return Result<CityEntity>.Failure("Something went wrong", System.Net.HttpStatusCode.InternalServerError);
        }
    }

    public Task<Result<bool>> Update(CityEntity model)
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

    public Task<Result<CityEntity>> FindById(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<CityEntity> GetAll()
    {
        return _context.Cities;
    }
}