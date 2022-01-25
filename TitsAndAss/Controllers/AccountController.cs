using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TitsAndAss.Models;

namespace TitsAndAss.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        [Route("")]
        [HttpGet("{id}")]
        [Authorize(Roles = "User")]
        public IActionResult OnLoged(int id)
        {
            using ApplicationContext db = new ApplicationContext();
            {
                return Ok(db.accounts.FirstOrDefault(a => a.Id == id));
            }
            return Unauthorized();
        }

        [Route("orders")]
        [HttpGet("orders/{id}")]
        [Authorize(Roles = "User")]
        public IActionResult Orders(int userIdentificator)
        {
            using ApplicationContext db = new ApplicationContext();
            {
                return Ok(db.orders.FirstOrDefault(a => a.Id == userIdentificator ));
            }
        }

        //[Route("historyorders")]
        //[HttpPost]
        //[Authorize(Roles = "User")]
        //public IActionResult HistoryOrders([FromBody] int id)
        //{
        //    using ApplicationContext db = new ApplicationContext();
        //    {
        //        return Ok(db.accounts.FirstOrDefault(a => a.Identificator == id));
        //    }
        //}




        [Route("changepassword")]
        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult ChangePassword([FromBody] Login login)
        {
            using ApplicationContext db = new ApplicationContext();
            {
                foreach (var l in db.accounts.ToList())
                {
                    if (l.Email == login.Email && l.Password == login.Password)
                    {
                        l.Password = login.Name;
                        db.SaveChanges();
                        return Ok(l);
                        //тестиииить
                    }
                }

            }
            return BadRequest();

        }

        [Route("changeemail")]
        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult ChangeEmail([FromBody] Login login)
        {
            using ApplicationContext db = new ApplicationContext();
            {
                foreach (var l in db.accounts.ToList())
                {
                    if (l.Email == login.Email && l.Password == login.Password)
                    {
                        l.Email = login.Name;
                        db.SaveChanges();
                        return Ok(l);
                        //тестиииить
                    }
                }

            }
            return BadRequest();
        }


        [Route("changename")]
        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult ChangeName([FromBody]Login login)
        {
            using ApplicationContext db = new ApplicationContext();
            {
                foreach (var l in db.accounts.ToList())
                {
                    if (l.Email == login.Email && l.Password == login.Password)
                    {
                        l.Name = login.Name;
                        db.SaveChanges();
                        return Ok(l);
                        //тестиииить
                    }
                }

            }
            return BadRequest();

        }

        [Route("addmoney")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult ChangeMoneyMul([FromBody] OperationCout model)
        {
            using ApplicationContext db = new ApplicationContext();
            {
                foreach (var l in db.accounts.ToList())
                {
                    if (l.Id == model.id)
                    {
                        l.Money = l.Money + model.count;
                        db.SaveChanges();
                        return Ok(l);
                        //тестиииить
                    }
                }

            }
            return BadRequest();

        }

        [Route("minusmoney")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult ChangeMoneyMin([FromBody] OperationCout model)
        {
            using ApplicationContext db = new ApplicationContext();
            {
                foreach (var l in db.accounts.ToList())
                {
                    if (l.Id== model.id)
                    {
                        l.Money = l.Money - model.count;
                        db.SaveChanges();
                        return Ok(l);
                        //добавить если <0
                    }
                }

            }
            return BadRequest();

        }
        //метод по изменению суммы

    }
}
