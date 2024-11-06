namespace Domain.Entities;

public class ShippingEntity : ActionsBase
{
    public int Id { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public string Status { get; set; }
    public DateTime ShippingDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public int DeliveryManId { get; set; }
    public DeliveryManEntity DeliveryMan { get; set; } = null!;
    public ICollection<PackageEntity> Package { get; } = new List<PackageEntity>();
}