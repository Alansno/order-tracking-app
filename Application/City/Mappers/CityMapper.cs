using Application.City.Dtos;
using Domain.Entities;

namespace Application.City.Mappers;

public class CityMapper
{
    public CityEntity ToEntity(CityRequest request)
    {
        return new CityEntity
        {
            CityName = request.CityName,
        };
    }

    public CityResponse ToDto(CityEntity cityEntity)
    {
        return new CityResponse
        {
            Id = cityEntity.Id,
            CityName = cityEntity.CityName,
        };
    }

    public IEnumerable<CityResponse> ToDtoList(List<CityEntity> cityEntities)
    {
        return cityEntities.Select(ToDto);
    }
}