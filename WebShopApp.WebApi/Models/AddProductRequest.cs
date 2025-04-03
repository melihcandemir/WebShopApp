using System.ComponentModel.DataAnnotations;

namespace WebShopApp.WebApi.Models
{
    public class AddProductRequest
    {
        [Required]
        [Length(3, 20)]
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}