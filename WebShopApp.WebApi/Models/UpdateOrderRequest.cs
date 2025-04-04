using System.ComponentModel.DataAnnotations;

namespace WebShopApp.WebApi.Models
{
    public class UpdateOrderRequest
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int Quentity { get; set; }
        public List<int> ProductIds { get; set; }
    }
}