using Microsoft.AspNetCore.Mvc;
using la_mia_pizzeria_razor_layout.Models;
using la_mia_pizzeria_razor_layout.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace la_mia_pizzeria_razor_layout.Controllers
{
    //[Authorize(Roles = "ADMIN,USER")]
    public class PizzaController : Controller
    {
        
        public IActionResult Index()
        {
            return View(PizzaManager.GetAllPizzas());
        }
        [Authorize(Roles = "ADMIN, USER")]
        public IActionResult Show(int id)
        {
            Pizza pizza = PizzaManager.GetPizzaById(id, true);
            if (pizza != null)
                return View(pizza);
            else
                return View("errore");
        }
        
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult Create() 
        {
            PizzaFormModel model = new PizzaFormModel();
            model.Pizza = new Pizza();
            model.Categories = PizzaManager.GetCategories();
            model.CreateIngredients();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]

        public IActionResult Create(PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                data.Categories = PizzaManager.GetCategories();
                data.CreateIngredients();
                return View("Create", data);
            }
       
            PizzaManager.InsertPizza(data.Pizza, data.SelectedIngredients);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]

        public IActionResult Update(int id)
        {
            Pizza pizzaToEdit = PizzaManager.GetPizzaById(id);
            if (pizzaToEdit == null)
            {
                return NotFound();
            }
            else
            {
                PizzaFormModel model = new PizzaFormModel(pizzaToEdit, PizzaManager.GetCategories());
                model.CreateIngredients();
                return View(model);
            }
          
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]

        public IActionResult Update(int id, PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                data.Categories=PizzaManager.GetCategories();
                data.CreateIngredients();
                data.Pizza.PizzaId = id; //Ripasso l'id di data per evitare che al salvataggio vada alla pagina con ID "0"
                //List<Category> categories = PizzaManager.GetCategories();
                //data.Categories = categories;
                return View("Update", data);
            }

            if (PizzaManager.UpdatePizza(id, data.Pizza.Name, data.Pizza.Description, data.Pizza.Photo, data.Pizza.Price, data.Pizza.CategoryId, data.SelectedIngredients))
                return RedirectToAction("Index");
            else
                return NotFound();

            //bool result = PizzaManager.UpdatePizza(id, pizzaToEdit =>
            //{
            //    pizzaToEdit.Name = data.Pizza.Name;
            //    pizzaToEdit.Description = data.Pizza.Description;
            //    pizzaToEdit.Photo = data.Pizza.Photo;
            //    pizzaToEdit.Price = data.Pizza.Price;
            //    pizzaToEdit.CategoryId = data.Pizza.CategoryId;
            //    pizzaToEdit.Ingredients.Clear();
            //});

            //if (result == true)
            //    return RedirectToAction("Index");
            //else
            //    return NotFound(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]

        public IActionResult Delete(int id)
        {
            if (PizzaManager.DeletePizza(id))
                return RedirectToAction("Index");
            else
                return NotFound();
        }
    }
}
