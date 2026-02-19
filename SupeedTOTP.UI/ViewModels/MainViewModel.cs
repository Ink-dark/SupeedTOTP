using ReactiveUI;
using System.Reactive;
using System.Collections.ObjectModel;
using SupeedTOTP.Core.Models;
using SupeedTOTP.Data.Repositories;
using SupeedTOTP.Data;
using System;

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
        Console.WriteLine("MainViewModel 构造函数开始");
        try
        {
            Console.WriteLine("正在初始化数据库...");
            _dbContext = new AppDbContext();
            _dbContext.Database.EnsureCreated();
            Console.WriteLine("数据库初始化成功");
            
            _accountRepository = new AccountRepository(_dbContext);
            Console.WriteLine("AccountRepository 创建成功");
            
            // 简化命令创建
            Console.WriteLine("正在创建ReactiveCommand...");
            AddAccountCommand = ReactiveCommand.Create(() => { 
                Console.WriteLine("AddAccountCommand 执行");
                /* 实现添加账号逻辑 */ 
            });
            SettingsCommand = ReactiveCommand.Create(() => { 
                Console.WriteLine("SettingsCommand 执行");
                /* 实现设置逻辑 */ 
            });
            EditAccountCommand = ReactiveCommand.Create<Account>(account => { 
                Console.WriteLine($"EditAccountCommand 执行: {account.Name}");
                /* 实现编辑账号逻辑 */ 
            });
            DeleteAccountCommand = ReactiveCommand.Create<Account>(account => { 
                Console.WriteLine($"DeleteAccountCommand 执行: {account.Name}");
                /* 实现删除账号逻辑 */ 
            });
            Console.WriteLine("所有命令创建完成");
            
            // 初始化一个示例账号，确保界面有内容显示
            Console.WriteLine("正在创建示例账号...");
            var sampleAccount = new Account
            {
                Name = "示例账号",
                Issuer = "示例服务商",
                Secret = "JBSWY3DPEHPK3PXP", // 示例密钥
                Digits = 6,
                Period = 30,
                Algorithm = "SHA1"
            };
            
            Console.WriteLine("正在创建AccountViewModel...");
            Accounts.Add(new AccountViewModel(sampleAccount));
            Console.WriteLine($"示例账号已添加到Accounts集合，总数: {Accounts.Count}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"MainViewModel 初始化失败: {ex.GetType().Name}: {ex.Message}");
            Console.WriteLine($"堆栈跟踪: {ex.StackTrace}");
            
            // 即使初始化失败，也创建一个空的视图模型，避免应用完全崩溃
            Console.WriteLine("使用安全模式继续...");
            // 创建空命令
            AddAccountCommand = ReactiveCommand.Create(() => { });
            SettingsCommand = ReactiveCommand.Create(() => { });
            EditAccountCommand = ReactiveCommand.Create<Account>(account => { });
            DeleteAccountCommand = ReactiveCommand.Create<Account>(account => { });
            
            // 添加一个最小化的示例账号
            var safeAccount = new Account { Name = "安全示例", Issuer = "系统", Secret = "TEST" };
            Accounts.Add(new AccountViewModel(safeAccount));
        }
        Console.WriteLine("MainViewModel 构造函数完成");
    }
}