using la_mia_pizzeria_razor_layout.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_razor_layout.Data
{
    public class PizzaManager
    {
        public static int CountAllPizzas()
        {
            using PizzaContext db = new PizzaContext();
            return db.Pizzas.Count();
        }
        public static List<Pizza> GetAllPizzas()
        {
            using PizzaContext db = new PizzaContext();
            return db.Pizzas.ToList();
        }
        public static List<Category> GetCategories()
        {
            using PizzaContext db = new PizzaContext();
            return db.Categories.ToList();
        }
        public static List<Ingredient> GetAllIngredients()
        {
            using PizzaContext db = new PizzaContext();
            return db.Ingredients.ToList();
        }
        public static Pizza GetPizzaById(int id, bool includeReferences = true)
        {
            using PizzaContext db = new PizzaContext();
            if (includeReferences)
                return db.Pizzas
                    .Where(p => p.PizzaId == id)
                    .Include(p => p.Category)
                    .Include(p => p.Ingredients)
                    .FirstOrDefault();
            return db.Pizzas
                .FirstOrDefault(p => p.PizzaId == id);
        }
        public static void InsertPizza(Pizza pizza, List<string> SelectedIngredients = null)
        {
            using PizzaContext db = new PizzaContext();
            if(SelectedIngredients != null)
            {
                pizza.Ingredients = new List<Ingredient>();

                foreach (var ingredientId in SelectedIngredients)
                {
                    int id = int.Parse(ingredientId);
                    var ingredient = db.Ingredients
                        .FirstOrDefault(i=>i.IngredientId == id);
                    pizza.Ingredients.Add(ingredient);
                }
            }
            db.Pizzas.Add(pizza);
            db.SaveChanges();
        }

        public static bool UpdatePizza(int id, Action<Pizza> edit)
        {
            using PizzaContext db = new PizzaContext();
            Pizza pizza = db.Pizzas
                .FirstOrDefault(p=>p.PizzaId ==id);

            if (pizza == null)
                return false;
            edit(pizza);
            db.SaveChanges();
            return true;
        }

        public static bool UpdatePizza(int id, string name, string description, string photo, decimal price, int? categoryId, List<string> ingredients)
        {
            using PizzaContext db = new PizzaContext();
            Pizza pizza = db.Pizzas
                .Where(p => p.PizzaId == id)
                .Include(p => p.Ingredients)
                .FirstOrDefault();

            if (pizza == null)
                return false;

            pizza.Name = name;
            pizza.Description = description;
            pizza.Photo = photo;
            pizza.Price = price;
            pizza.CategoryId = categoryId;

            pizza.Ingredients.Clear();
            if (ingredients != null)
            {
                foreach (var ingredient in ingredients)
                {
                    int ingredientId = int.Parse(ingredient);
                    var ingredientFromDB = db.Ingredients.FirstOrDefault(i => i.IngredientId == ingredientId);
                    pizza.Ingredients.Add(ingredientFromDB);
                }
            }
            db.SaveChanges();
            return true;
        }

        public static bool DeletePizza(int id)
        {
            using PizzaContext db = new PizzaContext();

            Pizza pizza = db.Pizzas
                .FirstOrDefault(p => p.PizzaId == id);

            if (pizza == null)
                return false;

            db.Pizzas.Remove(pizza);
            db.SaveChanges();

            return true;
        }

        public static void Seed()
        {
            if (CountAllPizzas() == 0)
            {
                InsertPizza(new Pizza("Margherita", "Pomodoro, fiordilatte e basilico", "~/img/margherita.jpg", 5.5m));
                InsertPizza(new Pizza("Marinara", "Pomodoro, origano ed aglio", "~/img/marinara.jpg", 4.5m));
                InsertPizza(new Pizza("Diavola", "Pomodoro, fiordilatte e salame piccante", "~/img/diavola.jpg", 6m));
                InsertPizza(new Pizza("Carrettiera", "Friarielli, fiordilatte e salsiccia", "~/img/salsiccia-friarielli.jpg", 6.5m));
                InsertPizza(new Pizza("Pizza fritta", "Pizza ripiena di cicoli, ricotta, mozzarella e pomodoro, fritta in olio evo", "~/img/fritta.jpg", 6m));
                InsertPizza(new Pizza("Bufalina", "Margherita con mozzarella di bufala dop", "~/img/bufalina.jpg", 6.5m));
            }
        }
    }
}
