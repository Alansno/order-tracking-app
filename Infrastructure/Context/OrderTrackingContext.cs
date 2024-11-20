using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class OrderTrackingContext : DbContext
{
    public OrderTrackingContext(DbContextOptions<OrderTrackingContext> options) : base(options)
    {
    }

    public DbSet<DeliveryManEntity> DeliveriesMan { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<PackageEntity> Packages { get; set; }
    public DbSet<ShippingEntity> Shippings { get; set; }
    public DbSet<CityEntity> Cities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<DeliveryManEntity>()
            .HasMany(d => d.Shipping)
            .WithOne(d => d.DeliveryMan)
            .HasForeignKey(d => d.DeliveryManId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<PackageEntity>()
            .HasMany(p => p.Product)
            .WithOne(p => p.Package)
            .HasForeignKey(p => p.PackageId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<ShippingEntity>()
            .HasMany(s => s.Package)
            .WithOne(s => s.Shipping)
            .HasForeignKey(s => s.ShippingId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<CityEntity>()
            .HasMany(s => s.Package)
            .WithOne(s => s.City)
            .HasForeignKey(s => s.CityId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<CityEntity>()
            .HasMany(s => s.Shipping)
            .WithOne(s => s.City)
            .HasForeignKey(s => s.CityId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ShippingEntity>()
            .Property(s => s.Status)
            .HasDefaultValue("Pendiente");

        modelBuilder.Entity<ShippingEntity>()
            .HasIndex(s => s.ShippingDate);

        modelBuilder.Entity<PackageEntity>()
            .HasIndex(p => p.Code)
            .IsUnique();
        
        modelBuilder.Entity<ShippingEntity>()
            .Property(s => s.DeliveryManId)
            .IsRequired(false);
        
        modelBuilder.Entity<PackageEntity>()
            .Property(s => s.ShippingId)
            .IsRequired(false); 
    }
}