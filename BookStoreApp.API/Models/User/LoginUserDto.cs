using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Models.User;

public class LoginUserDto
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}