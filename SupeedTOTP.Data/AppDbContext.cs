using Microsoft.EntityFrameworkCore;
using SupeedTOTP.Core.Models;
using System.IO;

namespace SupeedTOTP.Data;

public class AppDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // 使用当前工作目录，避免沙箱环境限制
        var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "totp.db");
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>()
            .HasKey(a => a.Id);
        
        modelBuilder.Entity<Account>()
            .Property(a => a.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        modelBuilder.Entity<Account>()
            .Property(a => a.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
