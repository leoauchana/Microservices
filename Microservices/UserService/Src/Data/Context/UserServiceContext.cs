using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class UserServiceContext : DbContext
{
    public UserServiceContext(DbContextOptions<UserServiceContext> options) : base(options)
    {

    }

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
            .Property(u => u.Email)
            .HasMaxLength(40)
            .IsRequired();
    }
}
