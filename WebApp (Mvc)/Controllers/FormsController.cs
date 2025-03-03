using Microsoft.AspNetCore.Mvc;

namespace CofeeShop.Controllers
{
    public class FormsController : Controller
    {
        public IActionResult Employee()
        {
            return View();
        }

        public IActionResult Department()
        {
            return View();
        }

        public IActionResult Project()
        {
            return View();
        }

        public IActionResult EmployeeProject()
        {
            return View();
        }
    }
}
