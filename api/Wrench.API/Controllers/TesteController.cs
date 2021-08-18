using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Wrench.API.Controllers
{
    public class TesteController : ControllerBase
    {
        public TesteController()
        {
        }

        [HttpGet("ComAutorizacao")]
        [Authorize]
        public ActionResult Get()
        {
            return Ok("Request com autorizacao");
        }

        [HttpGet("SemAutorizacao")]
        [AllowAnonymous]
        public ActionResult Get2()
        {
            return Ok("Request sem autorizacao");
        }
    }
}