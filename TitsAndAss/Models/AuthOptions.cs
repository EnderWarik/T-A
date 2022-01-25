using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TitsAndAss.Models
{
    public class AuthOptions
    {
        public string Issure { get; set; } //кто сгенерил

        public string Audience { get; set; }//для кого предназначается 

        public string Secret { get; set; } // секретная строка для генерации 

        public int TokenLifeTime { get; set; } // длительность токена в секундах

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
        }
    }
}
