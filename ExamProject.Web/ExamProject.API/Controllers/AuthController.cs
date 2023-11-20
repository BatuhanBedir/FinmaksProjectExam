using ExamProject.API.Application.DTOs;
using ExamProject.API.Application.IInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace ExamProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : CustomBaseController
{

    private readonly ILoginService _loginService;

    public AuthController(ILoginService loginService)
    {
        _loginService = loginService;
    }
    [HttpPost]
    public async Task<IActionResult> Login([FromForm] AppUserLoginDto appUserLoginDto)
    {
        var response = await _loginService.TokenHandler(appUserLoginDto);

        return CreateActionResultInstance(response);
    }
    [HttpGet]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public IActionResult GetLoggedInUserRole()
    {
        var role = User.FindFirstValue("Role");

        var response = CustomResponseDto<string>.Success(role, 200);

        return CreateActionResultInstance(response);
    }
}
