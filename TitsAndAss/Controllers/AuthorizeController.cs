using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TitsAndAss.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TitsAndAss.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {

        private readonly IOptions<AuthOptions> options;
        public AuthorizeController(IOptions<AuthOptions> options)
        {
            this.options = options;

        }


        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] Login login)
        {
            Console.WriteLine("To login");
            var user = AuthenticateUser(login.Email, login.Password);

            if (user != null)
            {
                var token = GenerateJWT(user);
                return Ok(new { access_token = token });
                //generate jwt
            }
            return Unauthorized();

        }

        private Account AuthenticateUser(string email, string password)
        {
            using ApplicationContext db = new ApplicationContext();
            {
                foreach (var l in db.accounts.ToList())
                {
                    if (l.Email == email && l.Password == password)
                    {
                        return l;
                    }
                }

            }
            return null;

        }


        [Route("Register")]
        [HttpPost]
        public IActionResult Register([FromBody] Login login)
        {
            if (login != null)
            {
                using ApplicationContext db = new ApplicationContext();
                {
                    foreach (var l in db.accounts.ToList())
                    {
                        if (l.Email == login.Email)
                        {
                            return BadRequest("AlredyExist"); //какой код то
                        }
                    }

                    db.accounts.Add(new Account { Email = login.Email, Password = login.Password, Role = "User", Name = login.Name });
                    db.SaveChanges();
                    return Ok();

                }

            }
            return BadRequest(); //шо це таке 
        }

        private string GenerateJWT(Account user)
        {
            var authParams = options.Value;
            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentinals = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
               // new Claim(ClaimsIdentity.DefaultRoleClaimType,"Admin"),
                new Claim("role","User")
                //new Claim(JwtRegisteredClaimNames.Iss,authParams.Issure)
            };
            //foreach(var role in user.Role)
            //{
            //    claims.Add(new Claim("role",role.ToString()));

            //}

            var token = new JwtSecurityToken(authParams.Issure,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifeTime),
                signingCredentials: credentinals);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
