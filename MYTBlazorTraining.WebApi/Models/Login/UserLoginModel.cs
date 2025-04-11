using System.ComponentModel.DataAnnotations;

namespace MYTBlazorTraining.WebApi.Models.Login
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Email is required"), EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required"), MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string? Password { get; set; }
    }
}
