using Application.City.Commands;

namespace Application.City;

public record class CityUseCases(
    AddCity AddCity,
    GetCities GetCities
    );