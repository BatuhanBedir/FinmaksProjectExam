using Microsoft.AspNetCore.Mvc;

namespace ExamProject.Web.Controllers;

public class AccountController : Controller
{
    public IActionResult Login()
    {
        return View();
    }
}
