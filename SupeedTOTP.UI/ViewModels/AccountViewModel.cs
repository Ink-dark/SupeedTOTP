using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using SupeedTOTP.Core.Models;
using SupeedTOTP.Core.Services;

namespace SupeedTOTP.UI.ViewModels
{
    public class AccountViewModel : INotifyPropertyChanged
    {
        private readonly Account _account;
        private readonly TotpService _totpService;
        private readonly CancellationTokenSource _cts;
        private readonly Task? _refreshTask;
        
        private string _currentToken = string.Empty;
        public string CurrentToken
        {
            get => _currentToken;
            private set => SetProperty(ref _currentToken, value);
        }
        
        private int _remainingSeconds;
        public int RemainingSeconds
        {
            get => _remainingSeconds;
            private set => SetProperty(ref _remainingSeconds, value);
        }
        
        public int RemainingSecondsPercentage => (int)((double)_remainingSeconds / _account.Period * 100);
        
        public string Name => _account.Name;
        public string Issuer => _account.Issuer;
        public Guid Id => _account.Id;
        
        public Account Model => _account;
        
        public ICommand CopyTokenCommand { get; }
        
        public event PropertyChangedEventHandler? PropertyChanged;
        
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
                return false;
            field = newValue;
            OnPropertyChanged(propertyName);
            return true;
        }
        
        public AccountViewModel(Account account)
        {
            Console.WriteLine($"AccountViewModel 构造函数开始: {account.Name}");
            
            // 初始化非空字段
            _account = account;
            _totpService = new TotpService();
            _cts = new CancellationTokenSource();
            
            // 初始化命令
            CopyTokenCommand = new RelayCommand(CopyToken);
            
            try
            {
                Console.WriteLine($"账号信息: Name={account.Name}, Issuer={account.Issuer}, Digits={account.Digits}, Period={account.Period}");
                Console.WriteLine("TotpService 创建成功");
                Console.WriteLine("CopyTokenCommand 创建成功");
                
                Console.WriteLine("正在刷新令牌...");
                RefreshToken();
                Console.WriteLine($"令牌刷新完成: {CurrentToken}, 剩余秒数: {RemainingSeconds}");
                
                // 启动后台任务，定期刷新令牌和剩余秒数
                Console.WriteLine("启动令牌刷新后台任务...");
                _refreshTask = Task.Run(async () => await RefreshTokenPeriodicallyAsync(_cts.Token), _cts.Token);
                Console.WriteLine("后台任务启动成功");
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
        
        private async Task RefreshTokenPeriodicallyAsync(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    // 等待1秒后刷新
                    await Task.Delay(1000, cancellationToken);
                    
                    // 刷新令牌和剩余秒数
                    RefreshToken();
                }
            }
            catch (OperationCanceledException)
            {
                // 任务被取消，正常退出
                Console.WriteLine($"账号 {_account.Name} 的令牌刷新任务已取消");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"账号 {_account.Name} 的令牌刷新任务失败: {ex.GetType().Name}: {ex.Message}");
            }
        }
        
        // 清理资源
        public void Dispose()
        {
            Console.WriteLine($"AccountViewModel.Dispose() 调用: {_account.Name}");
            _cts.Cancel();
            _cts.Dispose();
            _refreshTask?.Wait(1000); // 等待后台任务完成
        }
        
        public void RefreshToken()
        {
            try
            {
                Console.WriteLine("正在为账号 " + _account.Name + " 刷新令牌...");
                CurrentToken = _totpService.GenerateTotp(_account);
                RemainingSeconds = _totpService.GetRemainingSeconds(_account);
                OnPropertyChanged(nameof(RemainingSecondsPercentage));
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
        
        // 复制令牌到剪贴板
        public void CopyToken()
        {
            try
            {
                Console.WriteLine($"复制令牌到剪贴板: {CurrentToken}");
                System.Windows.Clipboard.SetText(CurrentToken);
                Console.WriteLine("令牌复制成功");
            }
            catch (Exception ex)
            {
                Console.WriteLine("复制令牌失败: " + ex.Message);
            }
        }
        
        // 转换运算符，用于在需要Account对象时自动转换
        public static implicit operator Account(AccountViewModel vm) => vm.Model;
    }
}
