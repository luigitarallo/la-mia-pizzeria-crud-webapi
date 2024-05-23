using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_razor_layout.Models
{
    public class Ingredient
    {
        [Key] public int IngredientId { get; set; }
        public string Name { get; set; }
        public List<Pizza> Pizzas { get; set; }
        public Ingredient() { }
    }
}
