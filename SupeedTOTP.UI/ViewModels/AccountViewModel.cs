using ReactiveUI;
using System.Reactive;
using SupeedTOTP.Core.Models;
using SupeedTOTP.Core.Services;
using System;

namespace SupeedTOTP.UI.ViewModels;

public class AccountViewModel : ReactiveObject
{
    private readonly Account _account;
    private readonly TotpService _totpService;
    
    private string _currentToken = string.Empty;
    public string CurrentToken
    {
        get => _currentToken;
        private set => this.RaiseAndSetIfChanged(ref _currentToken, value);
    }
    
    private int _remainingSeconds;
    public int RemainingSeconds
    {
        get => _remainingSeconds;
        private set => this.RaiseAndSetIfChanged(ref _remainingSeconds, value);
    }
    
    public int RemainingSecondsPercentage => (int)((double)_remainingSeconds / _account.Period * 100);
    
    public string Name => _account.Name;
    public string Issuer => _account.Issuer;
    public Guid Id => _account.Id;
    
    public Account Model => _account;
    
    public ReactiveCommand<Unit, Unit> CopyTokenCommand { get; }
    
    public AccountViewModel(Account account)
    {
        Console.WriteLine($"AccountViewModel 构造函数开始: {account.Name}");
        
        // 初始化非空字段
        _account = account;
        _totpService = new TotpService();
        CopyTokenCommand = ReactiveCommand.Create(() => {
            Console.WriteLine($"CopyTokenCommand 执行: {CurrentToken}");
            /* 实现复制令牌逻辑 */
            // Clipboard.SetTextAsync(CurrentToken);
        });
        
        try
        {
            Console.WriteLine($"账号信息: Name={account.Name}, Issuer={account.Issuer}, Digits={account.Digits}, Period={account.Period}");
            Console.WriteLine("TotpService 创建成功");
            Console.WriteLine("CopyTokenCommand 创建成功");
            
            Console.WriteLine("正在刷新令牌...");
            RefreshToken();
            Console.WriteLine($"令牌刷新完成: {CurrentToken}, 剩余秒数: {RemainingSeconds}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("AccountViewModel 初始化失败: " + ex.GetType().Name + ": " + ex.Message);
            Console.WriteLine("堆栈跟踪: " + ex.StackTrace);
            
            // 即使失败也设置默认值
            _currentToken = "ERROR";
            _remainingSeconds = 0;
        }
        Console.WriteLine("AccountViewModel 构造函数完成");
    }
    
    public void RefreshToken()
    {
        try
        {
            Console.WriteLine("正在为账号 " + _account.Name + " 刷新令牌...");
            CurrentToken = _totpService.GenerateTotp(_account);
            RemainingSeconds = _totpService.GetRemainingSeconds(_account);
            this.RaisePropertyChanged(nameof(RemainingSecondsPercentage));
            Console.WriteLine("令牌刷新成功: " + CurrentToken + ", 剩余秒数: " + RemainingSeconds);
        }
        catch (Exception ex)
        {
            Console.WriteLine("刷新令牌失败: " + ex.GetType().Name + ": " + ex.Message);
            Console.WriteLine("堆栈跟踪: " + ex.StackTrace);
            CurrentToken = "ERROR";
            RemainingSeconds = 0;
        }
    }
    
    // 转换运算符，用于在需要Account对象时自动转换
    public static implicit operator Account(AccountViewModel vm) => vm.Model;
}