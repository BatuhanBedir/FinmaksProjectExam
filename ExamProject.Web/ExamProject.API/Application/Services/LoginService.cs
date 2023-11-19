using ExamProject.API.Application.DTOs;
using ExamProject.API.Application.IInterfaces;
using ExamProject.API.Application.Providers;
using ExamProject.API.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace ExamProject.API.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtTokenProvider _jwtService;

        public LoginService(SignInManager<AppUser> signInManager, IJwtTokenProvider jwtService)
        {
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        public async Task<CustomResponseDto<string>> TokenHandler(AppUserLoginDto loginDto)
        {
            try
            {
                var user = await _signInManager.UserManager.FindByNameAsync(loginDto.UserName);

                if (user is null) return CustomResponseDto<string>.Fail("username or password is wrong", HttpStatusCode.NotFound.GetHashCode());

                var roles = await _signInManager.UserManager.GetRolesAsync(user);

                var signInResult = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

                if (!signInResult.Succeeded) return CustomResponseDto<string>.Fail("username or password is wrong", HttpStatusCode.NotFound.GetHashCode());

                var token = await _jwtService.GenerateToken(user.UserName, roles.First());

                return CustomResponseDto<string>.Success(token, HttpStatusCode.OK.GetHashCode());
            }
            catch (Exception ex)
            {

                return CustomResponseDto<string>.Fail($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError.GetHashCode());

            }
        }
    }
}
