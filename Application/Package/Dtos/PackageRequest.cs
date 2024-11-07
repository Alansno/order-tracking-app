using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Application.Package.Dtos;

public class PackageRequest
{
    [Required]
    public string Code { get; set; }
    public int? ShippingId { get; set; }
    [Required]
    public int CityId { get; set; }
}