using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using System.Collections.ObjectModel;
using SupeedTOTP.Core.Models;
using SupeedTOTP.Data.Repositories;
using SupeedTOTP.Data;

namespace SupeedTOTP.UI.ViewModels;

public class MainViewModel : ReactiveObject
{
    private readonly IAccountRepository _accountRepository;
    private readonly AppDbContext _dbContext;
    
    private string _searchQuery = string.Empty;
    public string SearchQuery
    {
        get => _searchQuery;
        set => this.RaiseAndSetIfChanged(ref _searchQuery, value);
    }
    
    private AccountViewModel? _selectedAccount;
    public AccountViewModel? SelectedAccount
    {
        get => _selectedAccount;
        set => this.RaiseAndSetIfChanged(ref _selectedAccount, value);
    }
    
    public ObservableCollection<AccountViewModel> Accounts { get; } = new();
    
    public ReactiveCommand<Unit, Unit> AddAccountCommand { get; }
    public ReactiveCommand<Unit, Unit> SettingsCommand { get; }
    public ReactiveCommand<Account, Unit> EditAccountCommand { get; }
    public ReactiveCommand<Account, Unit> DeleteAccountCommand { get; }
    
    public MainViewModel()
    {
        _dbContext = new AppDbContext();
        _dbContext.Database.EnsureCreated();
        _accountRepository = new AccountRepository(_dbContext);
        
        AddAccountCommand = ReactiveCommand.Create(() => { /* 实现添加账号逻辑 */ });
        SettingsCommand = ReactiveCommand.Create(() => { /* 实现设置逻辑 */ });
        EditAccountCommand = ReactiveCommand.Create<Account>(account => { /* 实现编辑账号逻辑 */ });
        DeleteAccountCommand = ReactiveCommand.Create<Account>(account => { /* 实现删除账号逻辑 */ });
        
        // 监听搜索查询变化
        this.WhenAnyValue(x => x.SearchQuery)
            .Throttle(TimeSpan.FromMilliseconds(300))
            .Subscribe(async query => await LoadAccountsAsync(query));
        
        LoadAccountsAsync().Wait();
        
        // 定期刷新令牌
        Observable.Interval(TimeSpan.FromSeconds(1))
            .Subscribe(_ => RefreshTokens());
    }
    
    private async Task LoadAccountsAsync(string query = "")
    {
        var accounts = await _accountRepository.SearchAsync(query);
        Accounts.Clear();
        
        foreach (var account in accounts)
        {
            Accounts.Add(new AccountViewModel(account));
        }
    }
    
    private void RefreshTokens()
    {
        foreach (var accountVm in Accounts)
        {
            accountVm.RefreshToken();
        }
    }
}
