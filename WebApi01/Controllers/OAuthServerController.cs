using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ObjectResult> Authorize(UserCredentials userCredentials)
        {
            if (userCredentials.Name == "mciec")
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaims(new List<Claim>() { new Claim(ClaimTypes.Name, userCredentials.Name), new Claim(ClaimTypes.Role, "user") });
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                return StatusCode(200, $"user {userCredentials.Name} authenticated");
            }
            else
            {
                return StatusCode(400, "Invalid user/password");
            }


        }



        // GET: api/OAuthServer
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/OAuthServer/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/OAuthServer
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/OAuthServer/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
