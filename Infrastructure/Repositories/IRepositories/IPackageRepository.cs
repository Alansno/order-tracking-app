using Domain.Entities;
using Infrastructure.Custom.ResultPattern;

namespace Infrastructure.Repositories.IRepositories;

public interface IPackageRepository
{
    Task<Result<bool>> AddShippingOnPackage(PackageEntity packageEntity, int shippingId);
}