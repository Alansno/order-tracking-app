using System.Collections.Immutable;
using System.Net;
using Domain.Entities;
using Infrastructure.Custom.ResultPattern;
using Infrastructure.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Application.Shipping.Services;

public class AssignShipmentService
{
    private readonly IRepository<ShippingEntity> _searchShipping;
    private readonly IRepository<PackageEntity> _searchPackage;
    private readonly IRepository<DeliveryManEntity> _searchDeliveryMan;
    private readonly IShippingRepository _shippingRepository;
    private readonly IPackageRepository _packageRepository;
    private readonly IDeliveryManRepository _deliveryManRepository;

    public AssignShipmentService(IRepository<ShippingEntity> searchShipping, IRepository<PackageEntity> searchPackage, IRepository<DeliveryManEntity> searchDeliveryMan, IShippingRepository shippingRepository, IPackageRepository packageRepository, IDeliveryManRepository deliveryManRepository)
    {
        _searchShipping = searchShipping;
        _searchPackage = searchPackage;
        _searchDeliveryMan = searchDeliveryMan;
        _shippingRepository = shippingRepository;
        _packageRepository = packageRepository;
        _deliveryManRepository = deliveryManRepository;
    }

    public async Task<Result<bool>> Execute()
    {
        var deliveryManActive = await _searchDeliveryMan.GetAll()
            .Where(d => d.Availability == true).FirstOrDefaultAsync();
        var shipping = await _searchShipping.GetAll()
            .Where(s => s.DeliveryManId == null && s.Status == "Pendiente").FirstOrDefaultAsync();

        if (deliveryManActive == null)
        {
            return Result<bool>.Failure("No deliveries active", HttpStatusCode.NotFound);
        }

        if (shipping == null)
        {
            return Result<bool>.Failure("No shipping", HttpStatusCode.NotFound);
        }

        var packages = await _searchPackage.GetAll()
            .Where(p => p.ShippingId == null && p.CityId == shipping.CityId).Take(deliveryManActive.NumPackages).ToListAsync();
        if (packages.Count == 0)
        {
            return Result<bool>.Failure("No packages available", HttpStatusCode.NotFound);
        }

        var availablePackages = await PackagesOnShipping(packages, shipping.Id);
        if (!availablePackages.IsSuccess)
        {
            return Result<bool>.Failure(availablePackages.Error, availablePackages.StatusCode);
        }

        var shipment = await _shippingRepository.CreateShipment(shipping, deliveryManActive.Id);
        if (!shipment.IsSuccess)
        {
            return Result<bool>.Failure(shipment.Error, shipment.StatusCode);
        }

        var deliveryManNotAvailable = await _deliveryManRepository.ChangeAvailability(deliveryManActive, false);
        if (!deliveryManNotAvailable.IsSuccess)
        {
            return Result<bool>.Failure(deliveryManNotAvailable.Error, deliveryManNotAvailable.StatusCode);
        }
        
        return Result<bool>.Success(true);
    }

    private async Task<Result<bool>> PackagesOnShipping(List<PackageEntity> packages, int shippingId)
    {
        foreach (var package in packages)
        {
            var availablePackage = await _packageRepository.AddShippingOnPackage(package, shippingId);
            if (!availablePackage.IsSuccess)
            {
                return Result<bool>.Failure(availablePackage.Error, availablePackage.StatusCode);
            }
        }

        return Result<bool>.Success(true);
    }
}