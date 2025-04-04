namespace WebShopApp.Business.Operations.Product.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}