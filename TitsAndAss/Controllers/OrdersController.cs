using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TitsAndAss.Models;

namespace TitsAndAss.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        // [Route("Create")]
        [HttpPost("create/{userId}")]
        [Authorize(Roles = "User")]
        public IActionResult Create(int userId)
        {
            using ApplicationContext db = new ApplicationContext();
            {
                Orders order = new Orders { Allcount = 0, ToUser = userId, Date = Convert.ToString(DateTime.Now) };
                db.orders.Add(order);
                db.SaveChanges();

                return Ok(order);
            }
            //return Unauthorized();
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "User")]
        public IActionResult GetOrder(int id)
        {
            using ApplicationContext db = new ApplicationContext();
            {
                foreach (var order in db.orders.ToList())
                {
                    if (order.Id == id)
                    {
                        return Ok(order);
                    }
                }


            }
            return NotFound();

        }

        [HttpGet("productsorder/{orderId}")]
        [Authorize(Roles = "User")]
        public IActionResult GetProductsInOrder(int orderId)
        {
            using ApplicationContext db = new ApplicationContext();
            {
                List<OrderProducts> list = new List<OrderProducts>();
                foreach (var product in db.userorders.ToList())
                {
                    if (product.IdOrder == orderId)
                    {
                        list.Add(product);


                    }
                }
                return Ok(list);

            }
            return NotFound();

        }

        //записал
       

        [Route("addproductinorder")]
        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult AddProductInOrder(ProductOrder model)
        {
            using ApplicationContext db = new ApplicationContext();
            {
                if (model != null)
                {
                    db.userorders.Add(new OrderProducts { count =db.products.FirstOrDefault(a=>a.Id==model.IdProduct).Count, IdOrder = model.IdOrder, IdProduct = model.IdProduct });
                    db.SaveChanges();
                    //Orders m=
                    RefreshCount(model.IdOrder, db.products.FirstOrDefault(a => a.Id == model.IdProduct).Count);
                    //if(m!=null)
                    return Ok();
                }
            }
            return NotFound();//статус код какой 
        }

        private Orders RefreshCount(int idOrder, int count)
        {
            using ApplicationContext db = new ApplicationContext();
            {
                foreach (var l in db.orders.ToList())
                {
                    if (l.Id == idOrder)
                    {
                        l.Allcount += count;
                        db.SaveChanges();
                        return l;
                    }
                }
            }
            return null;

        }




    }
}
