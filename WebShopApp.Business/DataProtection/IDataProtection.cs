namespace WebShopApp.Business.DataProtection
{
    public interface IDataProtection
    {
        // gönderilen metin şifrelenir
        string Protect(string text);


        // şifrelenmiş metin çözülür
        string Unprotect(string protectedText);
    }
}