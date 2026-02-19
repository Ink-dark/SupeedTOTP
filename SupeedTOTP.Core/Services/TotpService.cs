using OtpNet;
using SupeedTOTP.Core.Models;

namespace SupeedTOTP.Core.Services;

public class TotpService
{
    public string GenerateTotp(Account account)
    {
        var key = Base32Encoding.ToBytes(account.Secret);
        
        // 根据Otp.NET 1.4.0版本的API，正确的构造函数参数顺序
        // 参数1: 密钥, 参数2: 时间步长, 参数3: 哈希模式(默认Sha1), 参数4: 令牌长度
        var totp = new Totp(key, account.Period);
        
        return totp.ComputeTotp();
    }
    
    public bool VerifyTotp(Account account, string code, int window = 1)
    {
        var key = Base32Encoding.ToBytes(account.Secret);
        var totp = new Totp(key, account.Period);
        
        return totp.VerifyTotp(code, out long timeStepMatched, new VerificationWindow(window, window));
    }
    
    public int GetRemainingSeconds(Account account)
    {
        var key = Base32Encoding.ToBytes(account.Secret);
        var totp = new Totp(key, account.Period);
        return totp.RemainingSeconds();
    }
}
