using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi01.Controllers
{
    public class SomeClass
    {
        public int A { get; set; }
        public string B { get; set; }
        public SomeClass C { get; set; }
    }

    [Authorize]
    [ApiController]
    [Route("Api")]
    public class SampleController : ControllerBase
    {

        private readonly ILogger<SampleController> _logger;

        public SampleController(ILogger<SampleController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        [Route("[Action]")]
        public async Task<string> Get1()
        {
            string res = $"lalala authorized to {HttpContext.User.Identity.Name}.";
            foreach (var claim in HttpContext.User.Claims)
                res += $"{claim.Type}: {claim.Value}";
            return res;
        }

        [HttpGet]
        [Authorize]
        [Route("[Action]")]
        public async Task<ActionResult<SomeClass>> Get2()
        {
            SomeClass c = new SomeClass() { A = 1, B = "lalala", C = new SomeClass() { A = 2, B = "fiufiu", C = null } };
            var ret = new JsonResult(c);
            ret.StatusCode = 200;
            return Ok(c);
        }



    }
}
