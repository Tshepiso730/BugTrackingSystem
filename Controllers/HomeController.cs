// Controllers/HomeController.cs

using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    // GET: /
    public IActionResult Index()
    {
        return View();
    }

    // GET: /Home/About
    public IActionResult About()
    {
        ViewData["Message"] = "Your application description page.";

        return View();
    }

    // GET: /Home/Contact
    public IActionResult Contact()
    {
        ViewData["Message"] = "Your contact page.";

        return View();
    }

    // GET: /Home/Privacy
    public IActionResult Privacy()
    {
        return View();
    }
}
