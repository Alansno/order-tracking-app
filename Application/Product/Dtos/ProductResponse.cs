using Domain.Entities;

namespace Application.Product.Dtos;

public class ProductResponse
{
    public int Id { get; set; }
    public string NameProduct { get; set; }
    public PackageEntity? Package { get; set; }
}