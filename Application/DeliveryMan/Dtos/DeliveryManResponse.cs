namespace Application.DeliveryMan.Dtos;

public class DeliveryManResponse
{
    public int Id { get; set; }
    public string NameDelivery { get; set; }
    public bool Availability { get; set; }
    public int NumPackages { get; set; }
}