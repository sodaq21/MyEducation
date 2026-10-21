using Microsoft.AspNetCore.Mvc;

namespace HTTP_Practice2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetTestData()
        {
            return Ok("Any data");
        }
        [HttpDelete]
        public ActionResult DeleteTestData()
        {
            return Ok("Deleting data...");
        }
        [HttpPost]
        public ActionResult PostTestData()
        {
            return Ok("Creating data...");
        }
        [HttpPut]
        public ActionResult PutTestData()
        {
            return Ok("Updating data...");
        }
    }
}
