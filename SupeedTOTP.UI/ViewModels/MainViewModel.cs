using ReactiveUI;
using System.Reactive;
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
        // 简化构造函数，移除可能导致问题的异步调用和复杂逻辑
        _dbContext = new AppDbContext();
        _dbContext.Database.EnsureCreated();
        _accountRepository = new AccountRepository(_dbContext);
        
        // 简化命令创建
        AddAccountCommand = ReactiveCommand.Create(() => { /* 实现添加账号逻辑 */ });
        SettingsCommand = ReactiveCommand.Create(() => { /* 实现设置逻辑 */ });
        EditAccountCommand = ReactiveCommand.Create<Account>(account => { /* 实现编辑账号逻辑 */ });
        DeleteAccountCommand = ReactiveCommand.Create<Account>(account => { /* 实现删除账号逻辑 */ });
        
        // 初始化一个示例账号，确保界面有内容显示
        var sampleAccount = new Account
        {
            Name = "示例账号",
            Issuer = "示例服务商",
            Secret = "JBSWY3DPEHPK3PXP", // 示例密钥
            Digits = 6,
            Period = 30,
            Algorithm = "SHA1"
        };
        
        Accounts.Add(new AccountViewModel(sampleAccount));
    }
}
