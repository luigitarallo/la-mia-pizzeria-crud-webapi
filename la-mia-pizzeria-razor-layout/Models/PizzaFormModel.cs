using la_mia_pizzeria_razor_layout.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_razor_layout.Models
{
    public class PizzaFormModel
    {
        public Pizza Pizza { get; set; }
        public List<Category>? Categories { get; set; }

        public List<SelectListItem>? Ingredients { get; set; }
        public List<string>? SelectedIngredients { get; set; }

        public PizzaFormModel() { }
        public PizzaFormModel(Pizza pizza, List<Category> categories)
        {
            this.Pizza = pizza;
            this.Categories = categories;
        }

        public void CreateIngredients()
        {
            this.Ingredients = new List<SelectListItem>();
            this.SelectedIngredients = new List<string>();
            var ingredientsFromDb = PizzaManager.GetAllIngredients();
            foreach(var ingredient in ingredientsFromDb)
            {
                bool isSelected = this.Pizza.Ingredients?.Any(i => i.IngredientId == ingredient.IngredientId) == true;
                this.Ingredients.Add(new SelectListItem()
                {
                    Text = ingredient.Name,
                    Value = ingredient.IngredientId.ToString(),
                    Selected = isSelected
                });
                if(isSelected)
                    this.SelectedIngredients.Add(ingredient.IngredientId.ToString());
            }
        }
    }
}
