using Microsoft.AspNetCore.Mvc;

namespace Actio.Api.Controllers
{
  [Route("")]
  public class HomeController: Controller
  {
    [HttpGet]
    public IActionResult Get()
    {
        return Content("Hello from API");
    }
  }
}