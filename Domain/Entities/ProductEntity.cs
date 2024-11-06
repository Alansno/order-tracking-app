namespace Domain.Entities;

public class ProductEntity : ActionsBase
{
    public int Id { get; set; }
    public string NameProduct { get; set; }
    public string DescriptionProduct { get; set; }
    public int PackageId { get; set; }
    public PackageEntity Package { get; set; } = null!;
}