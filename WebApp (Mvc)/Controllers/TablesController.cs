using Microsoft.AspNetCore.Mvc;

namespace CofeeShop.Controllers
{
    public class TablesController : Controller
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
