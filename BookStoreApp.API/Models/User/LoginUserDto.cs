using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Models.User;

[Index("IX_Email",  IsUnique = true)]
[Index("IX_UserName",  IsUnique = true)]
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