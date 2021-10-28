using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snakes.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /*[Route("{name}/kekW/getAction")]
        [HttpGet]
        public ActionResult GetAction()
        {
            return new JsonResult(new { result = 0 });
        }*/
    }
}
