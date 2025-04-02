using System.ComponentModel.DataAnnotations;

namespace WebShopApp.WebApi.Models
{
    public class RegisterRequest
    {
        [Required] // girilmesi zorunlu
        [EmailAddress]
        public string Email { get; set; }
        [Required] // girilmesi zorunlu
        public string Password { get; set; }
        [Required] // girilmesi zorunlu
        public string FirstName { get; set; }
        [Required] // girilmesi zorunlu
        public string LastName { get; set; }
        [Required] // girilmesi zorunlu
        public string PhoneNumber { get; set; }
    }
}