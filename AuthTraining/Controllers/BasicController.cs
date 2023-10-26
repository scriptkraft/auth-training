using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace AuthTraining.Controllers
{
    [RoutePrefix("basic")]
    public class BasicController : ApiController
    {
        public override async Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            var authz = controllerContext.Request.Headers.Authorization;
            if (authz.Scheme == "Basic" && PerformBasicAuthentication(authz.Parameter))
            {
                return await base.ExecuteAsync(controllerContext, cancellationToken);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
        }

        private bool PerformBasicAuthentication(string encodedCredentials)
        {
            try
            {
                var bytes = Convert.FromBase64String(encodedCredentials);
                var decoded = System.Text.Encoding.UTF8.GetString(bytes);
                var credentials = decoded.Split(new[] { ':' }, 2);
                return VerifyCredentials(credentials[0], credentials[1]);
            }
            catch
            {
                return false;
            }
        }

        private bool VerifyCredentials(string username, string password)
        {
            return username == "testuser" && password == "testpass";
        }

        [Route("something")]
        [HttpGet]
        public async Task<IHttpActionResult> DoSomething()
        {
            return Ok("this function does something");
        }
    }
}
