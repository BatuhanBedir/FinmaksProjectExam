using Azure;
using ExamProject.API.Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomBaseController : ControllerBase
{
    public IActionResult CreateActionResultInstance<T>(CustomResponseDto<T> reponse)
    {
        return new ObjectResult(reponse)
        {
            StatusCode = reponse.StatusCode
        };
    }
}
