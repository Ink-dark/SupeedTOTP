using ReactiveUI;
using System.Reactive;
using SupeedTOTP.Core.Models;
using SupeedTOTP.Core.Services;

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
        _account = account;
        _totpService = new TotpService();
        
        CopyTokenCommand = ReactiveCommand.Create(() => {
            /* 实现复制令牌逻辑 */
            // Clipboard.SetTextAsync(CurrentToken);
        });
        
        RefreshToken();
    }
    
    public void RefreshToken()
    {
        CurrentToken = _totpService.GenerateTotp(_account);
        RemainingSeconds = _totpService.GetRemainingSeconds(_account);
        this.RaisePropertyChanged(nameof(RemainingSecondsPercentage));
    }
    
    // 转换运算符，用于在需要Account对象时自动转换
    public static implicit operator Account(AccountViewModel vm) => vm.Model;
}
