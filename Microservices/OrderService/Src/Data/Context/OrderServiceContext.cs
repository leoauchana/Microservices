using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class OrderServiceContext : DbContext
{
    public OrderServiceContext(DbContextOptions<OrderServiceContext> options) : base(options)
    {

    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> Items { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OrderConfig(modelBuilder);
        OrderItemConfig(modelBuilder);
    }
    private void OrderConfig(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .ToTable("Orders");
        modelBuilder.Entity<Order>()
            .HasKey(o => o.Id);
        modelBuilder.Entity<Order>()
            .Property(o => o.Number)
            .HasColumnType("int")
            .IsRequired();
        modelBuilder.Entity<Order>()
            .Property(o => o.Date)
            .HasColumnType("date")
            .IsRequired();
        modelBuilder.Entity<Order>()
            .Property(o => o.UserId)
            .HasColumnType("varchar")
            .HasMaxLength(50)
            .IsRequired();
    }
    private void OrderItemConfig(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderItem>()
            .ToTable("Order_Items");
        modelBuilder.Entity<OrderItem>()
            .HasKey(o => o.Id);
        modelBuilder.Entity<OrderItem>()
            .Property(o => o.ProductId)
            .HasColumnType("varchar")
            .HasMaxLength(50)
            .IsRequired();
        modelBuilder.Entity<OrderItem>()
            .Property(o => o.Quantity)
            .HasColumnType("int")
            .IsRequired();
    }
}
