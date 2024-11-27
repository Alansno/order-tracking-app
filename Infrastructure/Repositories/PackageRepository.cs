using System.Data;
using System.Linq.Expressions;
using System.Net;
using Dapper;
using Domain.Entities;
using Domain.Querys;
using Infrastructure.Context;
using Infrastructure.Custom.ResultPattern;
using Infrastructure.Repositories.IRepositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PackageRepository : IRepository<PackageEntity>, ISearchRepository<PackageEntity>, IPackageRepository
{
    private readonly OrderTrackingContext _context;
    private readonly DapperContext _dapperContext;
    public PackageRepository(OrderTrackingContext context, DapperContext dapperContext)
    {
        _context = context;
        _dapperContext = dapperContext;
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

    public async Task<Result<PackageEntity>> FindById(int id)
    {
        var package = await _context.Packages.FindAsync(id);
        if (package == null)
        {
            return Result<PackageEntity>.Failure("Package not found", System.Net.HttpStatusCode.NotFound);
        }

        return Result<PackageEntity>.Success(package);
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

    public async Task<Result<IEnumerable<PackageQuery>>> GetPackages()
    {
        var sql = @"SELECT p.Id, p.Code, c.CityName FROM Packages p LEFT JOIN Shippings s ON s.Id = p.ShippingId JOIN Cities c ON c.Id = p.CityId";
        using var connection = _dapperContext.GetConnection();
        var result = (await connection.QueryAsync<PackageQuery>(sql)).ToList();
    
        if (result.Count() == 0)
        {
            return Result<IEnumerable<PackageQuery>>.Failure("No packages found", HttpStatusCode.NotFound);
        }

        return Result<IEnumerable<PackageQuery>>.Success(result);
    }
}