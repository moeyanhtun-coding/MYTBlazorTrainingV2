using System.ComponentModel.DataAnnotations;

namespace MYTBlazorTraining.WebApi.Models.Register
{
    public class UserRegisterModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Email is required"), EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required"), MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required"),
            MinLength(6, ErrorMessage = "Confirm Password must be at least 6 characters long"),
            Compare("Password", ErrorMessage = "Password and Confirm Password do not match")
            ]
        public string? ConfirmPassword { get; set; }
    }
}
