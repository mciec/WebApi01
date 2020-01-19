using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebApi01.Controllers
{
    public class UserCredentials
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }


    [Route("OAuth2")]
    [ApiController]
    public class OAuthServerController : ControllerBase
    {
        [HttpPost]
        [Route("Authorize")]
        public async Task<string> Authorize(UserCredentials userCredentials)
        {
            if (userCredentials.Name == "mciec")
            {
                var key = Encoding.ASCII.GetBytes("YourKey-2374-OFFKDI940NG7:56753253-tyuw-5769-0921-kfirox29zoxv");
                var JWToken = new JwtSecurityToken(
                    issuer: "proper issuer",
                    audience: "michalC",
                    claims: new List<Claim>() { new Claim(ClaimTypes.Name, userCredentials.Name), new Claim(ClaimTypes.Role, "user") },
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                );
                var token = new JwtSecurityTokenHandler().WriteToken(JWToken);
                return token;
            }
            else
            {
                return null;
            }
        }
    }
}
