using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Data.Context;

public class UserServiceContext : DbContext
{
    public UserServiceContext(DbContextOptions<UserServiceContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        UserConfig(modelBuilder);
    }

    private void UserConfig(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<User>()
            .Property(u => u.Username)
            .HasMaxLength(20)
            .IsRequired();

        modelBuilder.Entity<User>()
        .Property(u => u.Password)
        .HasMaxLength(20)
        .IsRequired();

        modelBuilder.Entity<User>()
            .Property(a => a.Email)
            .HasConversion(
                email => email.Value,
                value => Email.Create(value)
            )
            .HasColumnType("varchar")
            .HasMaxLength(50)
            .IsRequired();
        modelBuilder.Entity<User>()
        .Property(u => u.FullName)
        .HasMaxLength(20)
        .IsRequired();
    }
}
