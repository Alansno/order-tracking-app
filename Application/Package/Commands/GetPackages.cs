using System.Net;
using Application.Package.Dtos;
using Application.Package.Mappers;
using Dapper;
using Domain.Entities;
using Infrastructure.Custom.ResultPattern;
using Infrastructure.Repositories.IRepositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Application.Package.Commands;

public class GetPackages
{
    private readonly IRepository<PackageEntity> _packageRepository;
    private readonly PackageMapper _mapper;
    private readonly IConfiguration _config;

    public GetPackages(IRepository<PackageEntity> packageRepository, PackageMapper mapper, IConfiguration config)
    {
        _packageRepository = packageRepository;
        _mapper = mapper;
        _config = config;
    }

    public async Task<Result<List<PackageResponse>>> Execute()
    {
        var connectionString = _config.GetConnectionString("Connection");
        await using var connection = new SqlConnection(connectionString);

        var sql = @"SELECT p.Id, p.Code, c.CityName FROM Packages p LEFT JOIN Shippings s ON s.Id = p.ShippingId JOIN Cities c ON c.Id = p.CityId";
        var result = await connection.QueryAsync<PackageResponse>(sql);

        if (result.Any())
        {
            return Result<List<PackageResponse>>.Success(result.ToList());
        }
    
        return Result<List<PackageResponse>>.Failure("No packages found", HttpStatusCode.NotFound);
    }
}