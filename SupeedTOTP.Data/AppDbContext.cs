using Microsoft.EntityFrameworkCore;
using SupeedTOTP.Core.Models;

namespace SupeedTOTP.Data;

public class AppDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SupeedTOTP", "totp.db");
        Directory.CreateDirectory(Path.GetDirectoryName(dbPath) ?? string.Empty);
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
