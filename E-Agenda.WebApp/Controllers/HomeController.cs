using Microsoft.AspNetCore.Mvc;

namespace E_Agenda.WebApp.Controllers;
public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewBag.Title = "Página Inicial";
        ViewBag.Header = "Bem-vindo a e-Agenda";
        return View(); 
    }
}
