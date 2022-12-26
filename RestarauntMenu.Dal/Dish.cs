using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestarauntMenu.Dal;

public sealed class DishEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public string? DishType { get; set; }
                 
    public string? Cuisine { get; set; }
                 
    public string? Ingredients { get; set; }

    public int? Votes { get; set; }

}

