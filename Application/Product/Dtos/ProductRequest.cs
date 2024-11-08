namespace Application.Product.Dtos;

public class ProductRequest
{
    public string NameProduct { get; set; }
    public string DescriptionProduct { get; set; }
    public int? PackageId { get; set; }
}