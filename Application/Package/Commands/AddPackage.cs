using Application.Package.Dtos;
using Application.Package.Mappers;
using Domain.Entities;
using Infrastructure.Custom.ResultPattern;
using Infrastructure.Repositories.IRepositories;

namespace Application.Package.Commands;

public class AddPackage
{
    private readonly IRepository<PackageEntity> _packageRepository;
    private readonly PackageMapper _mapper;

    public AddPackage(IRepository<PackageEntity> packageRepository, PackageMapper mapper)
    {
        _packageRepository = packageRepository;
        _mapper = mapper;
    }

    public async Task<Result<bool>> Execute(PackageRequest request)
    {
        var package = _mapper.ToEntity(request);
        var packageSaved = await _packageRepository.Save(package);

        if (packageSaved.IsSuccess)
        {
            return Result<bool>.Success(true);
        }

        return Result<bool>.Failure(packageSaved.Error, packageSaved.StatusCode);
    }
}