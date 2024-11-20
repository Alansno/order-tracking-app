using System.Linq.Expressions;
using Application.DeliveryMan.Dtos;
using Application.DeliveryMan.Mappers;
using Domain.Entities;
using Infrastructure.Custom.ResultPattern;
using Infrastructure.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Application.DeliveryMan.Commands;

public class GetDeliveryManWith
{
    private readonly ISearchRepository<DeliveryManEntity> _searchRepository;
    private readonly DeliveryManMapper _deliveryManMapper;

    public GetDeliveryManWith(ISearchRepository<DeliveryManEntity> searchRepository, DeliveryManMapper deliveryManMapper)
    {
        _searchRepository = searchRepository;
        _deliveryManMapper = deliveryManMapper;
    }

    public async Task<Result<List<DeliveryManResponse>>> Execute(Expression<Func<DeliveryManEntity, bool>> predicate)
    {
        var result = await _searchRepository.FilteredSearch(predicate);
        if (result.IsSuccess)
        {
            return Result<List<DeliveryManResponse>>.Success(_deliveryManMapper.ToDtoList(await result.Value.ToListAsync()));
        }

        return Result<List<DeliveryManResponse>>.Failure(result.Error, result.StatusCode);
    }
}