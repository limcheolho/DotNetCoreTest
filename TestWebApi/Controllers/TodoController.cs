using Microsoft.AspNetCore.Mvc;

namespace TestWebApi.Controllers;

public class TodoController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}