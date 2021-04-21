using Microsoft.AspNetCore.Mvc;

namespace Items.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InfoController
    {
        [HttpGet]
        public string Get() => "Hi";
    }
}
