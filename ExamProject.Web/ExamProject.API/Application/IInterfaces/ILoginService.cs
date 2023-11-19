using ExamProject.API.Application.DTOs;

namespace ExamProject.API.Application.IInterfaces;

public interface ILoginService
{
    public Task<CustomResponseDto<string>> TokenHandler(AppUserLoginDto loginDto);
}
