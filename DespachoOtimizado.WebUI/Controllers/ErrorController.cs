using Microsoft.AspNetCore.Mvc;

namespace DespachoOtimizado.WebUI.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Unauthorized()
        {
            return View();
        }
        public IActionResult ServerError()
        {
            return View();
        }
    }
}