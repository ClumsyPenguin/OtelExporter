using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace OtelExporter.Controllers
{
    [RoutePrefix("home")]
    public class HomeController : ApiController
    {
        [HttpGet, Route("")]
        public IHttpActionResult Test(CancellationToken cancellationToken = default)
            => Ok("Authenticated");
    }
}