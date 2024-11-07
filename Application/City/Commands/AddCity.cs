using Application.City.Dtos;
using Application.City.Mappers;
using Domain.Entities;
using Infrastructure.Custom.ResultPattern;
using Infrastructure.Repositories.IRepositories;

namespace Application.City.Commands;

public class AddCity
{
    private readonly IRepository<CityEntity> _cityRepository;
    private readonly CityMapper _cityMapper;

    public AddCity(IRepository<CityEntity> cityRepository, CityMapper cityMapper)
    {
        _cityRepository = cityRepository;
        _cityMapper = cityMapper;
    }

    public async Task<Result<bool>> Execute(CityRequest request)
    {
        var city = _cityMapper.ToEntity(request);
        var citySaved = await _cityRepository.Save(city);

        if (citySaved.IsSuccess)
        {
            return Result<bool>.Success(true);
        }

        return Result<bool>.Failure(citySaved.Error, citySaved.StatusCode);
    }
}