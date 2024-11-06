using System.ComponentModel.DataAnnotations;

namespace Application.DeliveryMan.Dtos;

public class DeliveryManRequest
{
    [Required]
    public string NameDelivery { get; set; }
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "El valor debe ser un número positivo")]
    public int NumPackages { get; set; }
}