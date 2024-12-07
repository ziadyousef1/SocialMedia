using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Social.Api.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok();

        }
    }
}
