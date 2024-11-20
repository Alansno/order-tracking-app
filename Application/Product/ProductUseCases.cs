using Application.Product.Commands;

namespace Application.Product;

public record class ProductUseCases(
    AddProduct AddProduct,
    GetProducts GetProducts,
    AddPackageInProduct AddPackageInProduct,
    GetProduct GetProduct
    );