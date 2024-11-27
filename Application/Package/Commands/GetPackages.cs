using System.Net;
using Application.Package.Dtos;
using Application.Package.Mappers;
using Dapper;
using Domain.Entities;
using Domain.Querys;
using Infrastructure.Custom.ResultPattern;
using Infrastructure.Repositories.IRepositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Application.Package.Commands;

public class GetPackages
{
    private readonly IPackageRepository _packageRepository;
    private readonly PackageMapper _mapper;

    public GetPackages(IPackageRepository packageRepository, PackageMapper mapper)
    {
        _packageRepository = packageRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<PackageQuery>>> Execute()
    {
        var packages = await _packageRepository.GetPackages();
        if (!packages.IsSuccess)
        {
            return Result<List<PackageQuery>>.Failure("No packages found", HttpStatusCode.NotFound);
        }

        return Result<List<PackageQuery>>.Success(packages.Value.ToList());
    }
}