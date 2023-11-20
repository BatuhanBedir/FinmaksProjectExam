using ExamProject.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ExamProject.Web.Controllers;

public class ExamController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public IActionResult GetArticleListPartial([FromBody] List<ArticleVm> articles)
    {
        //var model = JsonConvert.DeserializeObject<ArticleListVm>(jsonData);

        //HttpContext.Session.SetString("Role", articles.First().LoggedInUserRole);
        return PartialView("_ArticleListPartial", articles);
    }
    [HttpPost]
    public IActionResult GetArticlePartial([FromBody] GetContentVm content)
    {
        return PartialView("_ArticlePartial", content);
    }
}
