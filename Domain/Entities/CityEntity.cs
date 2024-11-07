namespace Domain.Entities;

public class CityEntity
{
    public int Id { get; set; }
    public string CityName { get; set; }
    public ICollection<PackageEntity> Package { get; } = new List<PackageEntity>();
    public ICollection<ShippingEntity> Shipping { get; } = new List<ShippingEntity>();
}