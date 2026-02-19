using OtpNet;
using SupeedTOTP.Core.Models;

namespace SupeedTOTP.Core.Services;

public class TotpService
{
    public string GenerateTotp(Account account)
    {
        var key = Base32Encoding.ToBytes(account.Secret);
        var totp = new Totp(
            key,
            account.Period,
            GetHashAlgorithm(account.Algorithm),
            account.Digits);
        
        return totp.ComputeTotp();
    }
    
    public bool VerifyTotp(Account account, string code, int window = 1)
    {
        var key = Base32Encoding.ToBytes(account.Secret);
        var totp = new Totp(
            key,
            account.Period,
            GetHashAlgorithm(account.Algorithm),
            account.Digits);
        
        return totp.VerifyTotp(code, out long timeStepMatched, new VerificationWindow(window, window));
    }
    
    public int GetRemainingSeconds(Account account)
    {
        var key = Base32Encoding.ToBytes(account.Secret);
        var totp = new Totp(key, account.Period);
        return totp.RemainingSeconds();
    }
    
    private OtpHashAlgorithm GetHashAlgorithm(string algorithm)
    {
        return algorithm.ToUpper() switch
        {
            "SHA1" => OtpHashAlgorithm.Sha1,
            "SHA256" => OtpHashAlgorithm.Sha256,
            "SHA512" => OtpHashAlgorithm.Sha512,
            _ => OtpHashAlgorithm.Sha1
        };
    }
}
