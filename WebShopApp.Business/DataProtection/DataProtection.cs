using Microsoft.AspNetCore.DataProtection;

namespace WebShopApp.Business.DataProtection
{
    public class DataProtection : IDataProtection
    {
        // kütüphane içerisinden çekiliyor
        private readonly IDataProtector _protector;

        public DataProtection(IDataProtectionProvider protector)
        {
            _protector = protector.CreateProtector("security");
        }

        public string Protect(string text)
        {
            return _protector.Protect(text);
        }

        public string Unprotect(string protectedText)
        {
            return _protector.Unprotect(protectedText);
        }
    }
}