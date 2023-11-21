using ExamProject.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

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
        return PartialView("_ArticleListPartial", articles);
    }
    [HttpPost]
    public IActionResult GetArticlePartial([FromBody] GetContentVm content)
    {
        return PartialView("_ArticlePartial", content);
    }
    [HttpPost]
    public IActionResult GetExamListPartial([FromBody] ExamListVm examListVm)
    {
        return PartialView("_ExamListPartial", examListVm);
    }

}
