using System.Net;
using Application.City.Dtos;
using Application.City.Mappers;
using Domain.Entities;
using Infrastructure.Custom.ResultPattern;
using Infrastructure.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Application.City.Commands;

public class GetCities
{
    private readonly IRepository<CityEntity> _cityRepository;
    private readonly CityMapper _cityMapper;

    public GetCities(IRepository<CityEntity> cityRepository, CityMapper cityMapper)
    {
        _cityRepository = cityRepository;
        _cityMapper = cityMapper;
    }

    public async Task<Result<IEnumerable<CityResponse>>> Execute()
    {
        var cities = _cityRepository.GetAll();
        var resCities = await cities.ToListAsync();

        if (resCities.Count() != 0)
        {
            return Result<IEnumerable<CityResponse>>.Success(_cityMapper.ToDtoList(resCities.ToList()));
        }

        return Result<IEnumerable<CityResponse>>.Failure("No cities", HttpStatusCode.NotFound);
    }
}