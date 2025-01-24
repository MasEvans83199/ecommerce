using System.ComponentModel.DataAnnotations;

namespace ECommercePortfolio.Models;

public class Product
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    
    [Range(0.01, 10000)]
    public decimal Price { get; set; }
    
    [StringLength(500)]
    public string Description { get; set; }
    public int CategoryId { get; set; }
    
    public Category Category { get; set; }
}