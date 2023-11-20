using ExamProject.API.Application.DTOs;
using ExamProject.API.Application.IInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;

namespace ExamProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExamController : CustomBaseController
{
    private readonly IRssService _rssService;
    private readonly IExamService _examService;

    public ExamController(IRssService rssService, IExamService examService)
    {
        _rssService = rssService;
        _examService = examService;
    }

    [HttpGet]
    public async Task<IActionResult> GetArticle()
    {
        var response = await _rssService.GetWiredArticle();
        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateExam([FromBody] ExamDto examDto)
    {
        return CreateActionResultInstance(await _examService.CreateExam(examDto));
    }
}
