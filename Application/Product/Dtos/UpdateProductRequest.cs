using System.ComponentModel.DataAnnotations;

namespace Application.Product.Dtos;

public class UpdateProductRequest
{
    [Required]
    public int PackageId { get; set; }
    [Required]
    public int ProductId { get; set; }
}