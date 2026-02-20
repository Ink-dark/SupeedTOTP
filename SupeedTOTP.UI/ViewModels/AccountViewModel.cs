using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SupeedTOTP.Core.Models;
using SupeedTOTP.Core.Services;

namespace SupeedTOTP.UI.ViewModels
{
    public class AccountViewModel : INotifyPropertyChanged
    {
        private readonly Account _account;
        private readonly TotpService _totpService;
        
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
