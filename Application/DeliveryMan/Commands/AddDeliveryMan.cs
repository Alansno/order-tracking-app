using Application.DeliveryMan.Dtos;
using Application.DeliveryMan.Mappers;
using Domain.Entities;
using Infrastructure.Custom.ResultPattern;
using Infrastructure.Repositories.IRepositories;

namespace Application.DeliveryMan.Commands;

public class AddDeliveryMan
{
   private readonly IRepository<DeliveryManEntity> _deliveryManRepository;
   private readonly DeliveryManMapper _mapper;

   public AddDeliveryMan(IRepository<DeliveryManEntity> deliveryManRepository, DeliveryManMapper mapper)
   {
      _deliveryManRepository = deliveryManRepository;
      _mapper = mapper;
   }

   public async Task<Result<DeliveryManResponse>> Execute(DeliveryManRequest request)
   {
      var deliveryMan = _mapper.ToEntity(request);
      var deliveryManSaved = await _deliveryManRepository.Save(deliveryMan);

      if (deliveryManSaved.IsSuccess)
      {
         return Result<DeliveryManResponse>.Success(_mapper.ToDto(deliveryManSaved.Value));
      }

      return Result<DeliveryManResponse>.Failure(deliveryManSaved.Error, deliveryManSaved.StatusCode);
   }
}