using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace SignalApp.Controllers
{
    public class DefaultController : ApiController
    {
        [HttpGet]
        public string Default()
        {
            return "text";
        }
    }
}
