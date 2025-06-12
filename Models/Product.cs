using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TestProduct.Models;

public class Product
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Guid Id { get; init; } = Guid.NewGuid();
    [Required]
    public string? Name { get; set; }
    [Column(TypeName ="decimal(18,2)")]
    [Range(0.01, 10000)]
    public decimal Price { get; set; }
    [Required]
    public string? Description { get; set; }
}
