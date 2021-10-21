using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using Entity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace ReadLater5.Areas.Identity.Pages.Account
{
    public class AuthController : Controller
    {
        private readonly SymmetricSecurityKey _key;
        public AuthController(IConfiguration conf)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conf["TokenKey"]));
        }
        [Route("[controller]/token")]
        public string CreateToken(ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.NameId,user.UserName)
            };
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
