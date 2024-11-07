using System.ComponentModel.DataAnnotations;

namespace Application.City.Dtos;

public class CityRequest
{
    [Required]
    public string CityName { get; set; }
}