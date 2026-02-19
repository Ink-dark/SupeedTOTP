using OtpNet;
using SupeedTOTP.Core.Models;

namespace SupeedTOTP.Core.Services;

public class TotpService
{
    public string GenerateTotp(Account account)
    {
        var key = Base32Encoding.ToBytes(account.Secret);
        
        // 根据Otp.NET 1.4.0版本的API，使用简单的构造函数
        var totp = new Totp(key, account.Period, account.Digits);
        
        return totp.ComputeTotp();
    }
    
    public bool VerifyTotp(Account account, string code, int window = 1)
    {
        var key = Base32Encoding.ToBytes(account.Secret);
        var totp = new Totp(key, account.Period, account.Digits);
        
        return totp.VerifyTotp(code, out long timeStepMatched, new VerificationWindow(window, window));
    }
    
    public int GetRemainingSeconds(Account account)
    {
        var key = Base32Encoding.ToBytes(account.Secret);
        var totp = new Totp(key, account.Period);
        return totp.RemainingSeconds();
    }
}
