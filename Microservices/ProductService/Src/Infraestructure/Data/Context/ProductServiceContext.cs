using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Infraestructure.Data.Context;

public class ProductServiceContext : DbContext
{
    public ProductServiceContext(DbContextOptions<ProductServiceContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ProductConfig(modelBuilder);
    }

    private void ProductConfig(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().ToTable("Products");
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name)
            .HasMaxLength(50)
            .IsRequired();
            entity.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(200);
            entity.Property(e => e.Price)
            .HasColumnType("float")
            .IsRequired();
            entity.Property(e => e.Stock)
            .HasColumnType("int")
            .IsRequired();
        });
    }


}
