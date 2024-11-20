using System.Net;
using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Custom.ResultPattern;
using Infrastructure.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository : IRepository<ProductEntity>, IProductRepository
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

    public async Task<Result<ProductEntity>> FindById(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return Result<ProductEntity>.Failure("Product not found", System.Net.HttpStatusCode.NotFound);
        }

        return Result<ProductEntity>.Success(product);
    }

    public IQueryable<ProductEntity> GetAll()
    {
        return _context.Products;
    }
    
    public async Task<Result<ProductEntity>> UpdateProduct(int packageId, int productId)
    {
        var product = await FindById(productId);
        if (product.IsSuccess)
        {
            if (product.Value.PackageId == packageId)
            {
                return Result<ProductEntity>
                    .Failure("The product has already been assigned that package", HttpStatusCode.Conflict);
            }
            product.Value.PackageId = packageId;
            product.Value.UpdatedAt = DateTime.Now;
            
            _context.Entry(product.Value).Property(p => p.PackageId).IsModified = true;
            _context.Entry(product.Value).Property(p => p.UpdatedAt).IsModified = true;

            return Result<ProductEntity>.Success(product.Value);
        }

        return Result<ProductEntity>.Failure(product.Error, product.StatusCode);
    }
}