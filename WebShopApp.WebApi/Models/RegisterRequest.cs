using System.ComponentModel.DataAnnotations;

namespace WebShopApp.WebApi.Models
{
    public class RegisterRequest
    {
        [Required] // required entry
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}