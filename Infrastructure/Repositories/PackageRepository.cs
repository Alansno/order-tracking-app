using System.Data;
using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Custom.ResultPattern;
using Infrastructure.Repositories.IRepositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PackageRepository : IRepository<PackageEntity>
{
    private readonly OrderTrackingContext _context;

    public PackageRepository(OrderTrackingContext context)
    {
        _context = context;
    }
    
    public async Task<Result<PackageEntity>> Save(PackageEntity model)
    {
        try
        {
            if (model == null)
                return Result<PackageEntity>.Failure("Model was found", System.Net.HttpStatusCode.NotFound);

            await _context.Packages.AddAsync(model);
            await _context.SaveChangesAsync();
            return Result<PackageEntity>.Success(model);
        }
        catch (DbUpdateException ex)
        {
            return Result<PackageEntity>.Failure("El código del paquete ya existe", System.Net.HttpStatusCode.Conflict);
        }
    }

    public Task<Result<bool>> Update(PackageEntity model)
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

    public Task<Result<PackageEntity>> FindById(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<PackageEntity> GetAll()
    {
        throw new NotImplementedException();
    }
}