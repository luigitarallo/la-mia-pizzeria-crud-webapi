using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_razor_layout.Models
{
    public class Category
    {
        [Key] public int Id { get; set; } 
        public string Name { get; set; }
        public List<Pizza> Pizzas { get; set; }

        public Category() { }
    }
}
