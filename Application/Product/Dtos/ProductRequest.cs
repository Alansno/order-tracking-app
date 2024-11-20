using System.ComponentModel.DataAnnotations;

namespace Application.Product.Dtos;

public class ProductRequest
{
    [Required]
    public string NameProduct { get; set; }
    [Required]
    public string DescriptionProduct { get; set; }
    public int? PackageId { get; set; }
}