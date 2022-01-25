using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TitsAndAss.Models;

namespace TitsAndAss.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [Route("create")]
        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult CreateProduct(ProductPost model)
        {
            using ApplicationContext db = new ApplicationContext();
            {
                Products products = new Products
                {
                    Count = model.Count,
                    FromUser = model.FromUser,
                    Name = model.Name,
                    Date = Convert.ToString(DateTime.Now)
                };
                db.products.Add(products);
                db.SaveChanges();

                return Ok(products);
            }
            //return NotFound();//статус код какой 
        }
        //записал
        [HttpGet("{id}")]
        [Authorize(Roles = "User")]
        public IActionResult GetProduct(int id)
        {
            using ApplicationContext db = new ApplicationContext();
            {
                return Ok(db.products.FirstOrDefault(a => a.Id == id));
            }
            //return NotFound();//статус код какой 
        }
    }
}
