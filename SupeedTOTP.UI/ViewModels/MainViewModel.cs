using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SupeedTOTP.Core.Models;

namespace SupeedTOTP.UI.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _searchQuery = string.Empty;
        public string SearchQuery
        {
            get => _searchQuery;
            set => SetProperty(ref _searchQuery, value);
        }
        
        private AccountViewModel? _selectedAccount;
        public AccountViewModel? SelectedAccount
        {
            get => _selectedAccount;
            set => SetProperty(ref _selectedAccount, value);
        }
        
        public ObservableCollection<AccountViewModel> Accounts { get; } = [];
        
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
        
        public MainViewModel()
        {
            Console.WriteLine("MainViewModel 构造函数开始");
            
            try
            {
                Console.WriteLine("MainViewModel: 跳过数据库初始化");
                
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
                Console.WriteLine("示例账号已添加到Accounts集合，总数: " + Accounts.Count);
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainViewModel 初始化失败: " + ex.GetType().Name + ": " + ex.Message);
                Console.WriteLine("堆栈跟踪: " + ex.StackTrace);
            }
            Console.WriteLine("MainViewModel 构造函数完成");
        }
        
        // 命令方法
        public void AddAccount()
        {
            Console.WriteLine("AddAccount 执行");
        }
        
        public void EditAccount(Account account)
        {
            Console.WriteLine("EditAccount 执行: " + account.Name);
        }
        
        public void DeleteAccount(Account account)
        {
            Console.WriteLine("DeleteAccount 执行: " + account.Name);
        }
        
        public void OpenSettings()
        {
            Console.WriteLine("OpenSettings 执行");
        }
    }
}
