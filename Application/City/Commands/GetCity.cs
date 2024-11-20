using Application.City.Dtos;
using Application.City.Mappers;
using Domain.Entities;
using Infrastructure.Custom.ResultPattern;
using Infrastructure.Repositories.IRepositories;

namespace Application.City.Commands;

public class GetCity
{
    private readonly IRepository<CityEntity> _cityRepository;
    private readonly CityMapper _cityMapper;

    public GetCity(IRepository<CityEntity> cityRepository, CityMapper cityMapper)
    {
        _cityRepository = cityRepository;
        _cityMapper = cityMapper;
    }

    public async Task<Result<CityResponse>> Execute(int cityId)
    {
        var city = await _cityRepository.FindById(cityId);
        if (city.IsSuccess)
        {
            return Result<CityResponse>.Success(_cityMapper.ToDto(city.Value));
        }
        
        return Result<CityResponse>.Failure(city.Error, city.StatusCode);
    }
}