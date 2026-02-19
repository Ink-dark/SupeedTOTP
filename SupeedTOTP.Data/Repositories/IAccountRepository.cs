using SupeedTOTP.Core.Models;

namespace SupeedTOTP.Data.Repositories;

public interface IAccountRepository
{
    Task<Account> GetByIdAsync(Guid id);
    Task<IEnumerable<Account>> GetAllAsync();
    Task AddAsync(Account account);
    Task UpdateAsync(Account account);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<Account>> SearchAsync(string query);
}
