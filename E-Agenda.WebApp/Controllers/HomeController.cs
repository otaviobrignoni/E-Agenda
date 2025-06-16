using Microsoft.AspNetCore.Mvc;

namespace E_Agenda.WebApp.Controllers;
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
