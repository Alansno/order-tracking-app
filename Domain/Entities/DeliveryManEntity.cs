namespace Domain.Entities;

public class DeliveryManEntity
{
    public int Id { get; set; }
    public string NameDelivery { get; set; }
    public bool Availability { get; set; }
    public int NumPackages { get; set; }
    public ICollection<ShippingEntity> Shipping { get; } = new List<ShippingEntity>();
}