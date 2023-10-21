using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AuthTraining.Controllers
{
    [RoutePrefix("basic")]
    public class BasicController : ApiController
    {
        [Route("something")]
        [HttpGet]
        public async Task<IHttpActionResult> DoSomething()
        {
            return Ok("this function does something");
        }
    }
}
