namespace WebShopApp.Business.Types
{
    public class ServisMessage
    {
        public bool IsSucceed { get; set; }
        public string Message { get; set; }
    }

    public class ServisMessage<T>
    {
        public bool IsSucceed { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
    }
}