namespace Domain.Entities;

public class PackageEntity : ActionsBase
{
    public int Id { get; set; }
    public string Code { get; set; }
    public int? ShippingId { get; set; }
    public int CityId { get; set; }
    public ShippingEntity Shipping { get; set; } = null!;
    public CityEntity City { get; set; } = null!;
    public ICollection<ProductEntity> Product { get; } = new List<ProductEntity>();
}