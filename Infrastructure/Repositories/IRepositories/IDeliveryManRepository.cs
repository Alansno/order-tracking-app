using Domain.Entities;
using Infrastructure.Custom.ResultPattern;

namespace Infrastructure.Repositories.IRepositories;

public interface IDeliveryManRepository
{
    Task<Result<bool>> ChangeAvailability(DeliveryManEntity deliveryMan, bool condition);
}