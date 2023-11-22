using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ExamProject.Web.Controllers;

public class AccountController : Controller
{
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Logout()
    {
        HttpContext.SignOutAsync();
        HttpContext.Session.Remove("email");
        Response.Cookies.Delete(".AspNetCore.Session");
        return RedirectToAction("Index", "Home");
    }
}
