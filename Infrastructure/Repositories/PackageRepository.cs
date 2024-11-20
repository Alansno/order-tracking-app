using System.Data;
using System.Linq.Expressions;
using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Custom.ResultPattern;
using Infrastructure.Repositories.IRepositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PackageRepository : IRepository<PackageEntity>, ISearchRepository<PackageEntity>, IPackageRepository
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
            return Result<PackageEntity>.Failure("Something went wrong", System.Net.HttpStatusCode.Conflict);
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
        return _context.Packages;
    }

    public async Task<Result<IQueryable<PackageEntity>>> FilteredSearch(Expression<Func<PackageEntity, bool>> predicate)
    {
        var packages = _context.Packages.Where(predicate);
        if (!await packages.AnyAsync())
        {
            return Result<IQueryable<PackageEntity>>.Failure("No packages found", System.Net.HttpStatusCode.NotFound);
        }

        return Result<IQueryable<PackageEntity>>.Success(packages);
    }

    public async Task<Result<bool>> AddShippingOnPackage(PackageEntity packageEntity, int shippingId)
    {
        packageEntity.ShippingId = shippingId;
        packageEntity.UpdatedAt = DateTime.Now;
        
        _context.Entry(packageEntity).Property(p => p.ShippingId).IsModified = true;
        _context.Entry(packageEntity).Property(p => p.UpdatedAt).IsModified = true;
        await _context.SaveChangesAsync();
        return Result<bool>.Success(true);
    }
}