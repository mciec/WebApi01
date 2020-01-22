﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi02.Controllers
{
    [Route("[Action]")]
    [ApiController]
    public class MyController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Login(string name, string password)
        {
            if (name=="mciec")
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, name) };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);

                //Just redirect to our index after logging in. 
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


    }
}
