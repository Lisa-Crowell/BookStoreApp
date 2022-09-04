using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Models.Author;

public class AuthorCreateDto
{
    [Required]
    [StringLength(55)]
    public string? FirstName { get; set; }
    [Required]
    [StringLength(55)]
    public string? LastName { get; set; }
    [StringLength(255)]
    public string? Bio { get; set; }
}