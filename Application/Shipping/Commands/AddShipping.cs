using Application.City.Commands;
using Application.Shipping.Dtos;
using Application.Shipping.Mappers;
using Domain.Entities;
using Infrastructure.Custom.ResultPattern;
using Infrastructure.Repositories.IRepositories;

namespace Application.Shipping.Commands;

public class AddShipping
{
    private readonly IRepository<ShippingEntity> _shippingRepository;
    private readonly ShippingMapper _shippingMapper;
    private readonly GetCity _getCity;

    public AddShipping(IRepository<ShippingEntity> shippingRepository, ShippingMapper shippingMapper, GetCity getCity)
    {
        _shippingRepository = shippingRepository;
        _shippingMapper = shippingMapper;
        _getCity = getCity;
    }

    public async Task<Result<ShippingResponse>> Execute(ShippingRequest request)
    {
        var city = await _getCity.Execute(request.CityId);
        if (!city.IsSuccess)
        {
            return Result<ShippingResponse>.Failure(city.Error, city.StatusCode);
        }
        
        ShippingEntity shipping = _shippingMapper.ToEntity(request, city.Value.Id);
        var shippingSaved = await _shippingRepository.Save(shipping);

        if (shippingSaved.IsSuccess)
        {
            return Result<ShippingResponse>.Success(_shippingMapper.ToDto(shippingSaved.Value));
        }
        
        return Result<ShippingResponse>.Failure(shippingSaved.Error, shippingSaved.StatusCode);
    }
}