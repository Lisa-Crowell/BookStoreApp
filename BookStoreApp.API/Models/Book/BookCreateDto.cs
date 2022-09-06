using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Models.Book;

public class BookCreateDto
{
    [Required]
    [StringLength(55)]
    public string? Title { get; set; }
    [Required]
    public string Isbn { get; set; }
    [Required]
    [StringLength(255), MinLength(10)]
    public string? Summary { get; set; }
    
    public string? Image { get; set; }
    [Required]
    [Range(0, int.MaxValue)]
    public decimal? Price { get; set; }
    [Required]
    [Range(0, int.MaxValue)]
    public int? Year { get; set; }
}