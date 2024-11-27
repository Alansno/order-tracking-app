namespace Application.Shipping.Dtos;

public class ShippingResponse
{
    public int Id { get; set; }
    public string Origin { get; set; }
    public string Status { get; set; }
    public DateTime? DeliveryDate { get; set; }
}