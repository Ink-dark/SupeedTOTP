using Microsoft.EntityFrameworkCore;
using SupeedTOTP.Core.Models;

namespace SupeedTOTP.Data.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _context;
    
    public AccountRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Account> GetByIdAsync(Guid id)
    {
        return await _context.Accounts.FindAsync(id) ?? throw new KeyNotFoundException();
    }
    
    public async Task<IEnumerable<Account>> GetAllAsync()
    {
        return await _context.Accounts.ToListAsync();
    }
    
    public async Task AddAsync(Account account)
    {
        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(Account account)
    {
        account.UpdatedAt = DateTime.UtcNow;
        _context.Accounts.Update(account);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(Guid id)
    {
        var account = await _context.Accounts.FindAsync(id);
        if (account != null)
        {
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task<IEnumerable<Account>> SearchAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return await GetAllAsync();
        }
        
        return await _context.Accounts
            .Where(a => a.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                        a.Issuer.Contains(query, StringComparison.OrdinalIgnoreCase))
            .ToListAsync();
    }
}
