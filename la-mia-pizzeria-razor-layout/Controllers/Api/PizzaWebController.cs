using la_mia_pizzeria_razor_layout.Data;
using la_mia_pizzeria_razor_layout.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_razor_layout.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PizzaWebController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllPizzas(string? name)
        {
            if (name == null)
            {
                return Ok(PizzaManager.GetAllPizzas());
            }
            return Ok(PizzaManager.GetAllPizzas(name));
        }

        [HttpGet]
        public IActionResult GetPizzaById(int id)
        {
            Pizza pizza = PizzaManager.GetPizzaById(id);
            if(pizza == null)
            {
                return NotFound();
            }
            return Ok(pizza);
        }

        [HttpPost]
        public IActionResult PizzaPost([FromBody] Pizza pizza)
        {
            PizzaManager.InsertPizza(pizza);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult PizzaPut(int id, [FromBody] Pizza pizza)
        {
            Pizza oldPizza = PizzaManager.GetPizzaById(id);
            if(oldPizza == null)
                return NotFound();
            PizzaManager.UpdatePizza(id, pizza.Name, pizza.Description, pizza.Photo, pizza.Price, pizza.CategoryId, null);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult PizzaDelete(int id)
        {
            bool found = PizzaManager.DeletePizza(id);
            if(!found)
                return NotFound();
            return Ok();
        }
    }
}
