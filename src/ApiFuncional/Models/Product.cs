using System.ComponentModel.DataAnnotations;

namespace ApiFuncional.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [Range(1, int.MaxValue, ErrorMessage = "O campo {0} deve ser maior ou igual a {1}.")]
    public decimal Price { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public int StockQuantity { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string Description { get; set; }
}