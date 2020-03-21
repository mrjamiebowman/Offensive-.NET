using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Complex.Controllers
{
    [Route("api/")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        // GET: api/Default
        [HttpGet]
        public string Get()
        {
            return "";
        }

        // GET: api/Default/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(string id)
        {
            return "id";

        }
    }
}
