using Application.Package.Commands;

namespace Application.Package;

public record class PackageUseCases(
    AddPackage AddPackage,
    GetPackages GetPackages
    );