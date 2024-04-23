using Microsoft.EntityFrameworkCore;
using InventoryMate.Models;

namespace InventoryMate.Data;

public class AppDbContext : DbContext
{
    public AppDbContext (DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Notification> Notifications { get; set; }

    public DbSet<Sale> Sales { get; set; }

    public DbSet<Transaction> Transactions { get; set; }

    public DbSet<Store> Stores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Store>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(s => s.OwnerId);

        modelBuilder.Entity<Sale>()
            .HasOne<Product>()
            .WithMany()
            .HasForeignKey(s => s.ProductId);
        
        modelBuilder.Entity<Sale>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(s => s.UserId);
        
        modelBuilder.Entity<Sale>()
            .HasOne<Store>()
            .WithMany()
            .HasForeignKey(s => s.StoreId);

        modelBuilder.Entity<Notification>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(n => n.UserId);
        
        modelBuilder.Entity<Product>()
            .HasOne<Store>()
            .WithMany()
            .HasForeignKey(p => p.StoreId);
        
        modelBuilder.Entity<Transaction>()
            .HasOne<Product>()
            .WithMany()
            .HasForeignKey(t => t.ProductId);
        
        modelBuilder.Entity<Transaction>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(t => t.UserId);
        
        modelBuilder.Entity<Transaction>()
            .HasOne<Store>()
            .WithMany()
            .HasForeignKey(t => t.StoreId);

        base.OnModelCreating(modelBuilder);
    }

}