using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ProgrammersBlog.API.Controllers
{
    [Route("homes")]
    [ApiController]
    public class HomesController : ControllerBase
    {
        [HttpGet("{id}")]
        public object Index(string id)
        {
            return new 
            { 
                Name = $"home {id}",
                StartupHeader = Request.Headers["StartupHeader"],
                CtorHeader = Request.Headers["Middleware-Ctor"],
                MethodHeader = Request.Headers["Middleware-Method"]
            };
        }
    }
}
