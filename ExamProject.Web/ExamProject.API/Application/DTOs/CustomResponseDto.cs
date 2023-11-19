using System.Text.Json.Serialization;

namespace ExamProject.API.Application.DTOs;

public class CustomResponseDto<T>
{
    public T? Data { get; set; }
    [JsonIgnore]
    public int StatusCode { get; private set; }

    [JsonIgnore]
    public bool IsSuccessful { get; private set; }
    public List<string>? Errors { get; set; }

    //static fact. 
    public static CustomResponseDto<T> Success(T data, int statusCode)
    {
        return new CustomResponseDto<T> { Data = data, StatusCode = statusCode, IsSuccessful = true };
    }
    public static CustomResponseDto<T> Success(int statusCode)
    {
        return new CustomResponseDto<T> { Data = default(T), StatusCode = statusCode, IsSuccessful = true };
    }
    public static CustomResponseDto<T> Fail(List<string> errors, int statusCode)
    {
        return new CustomResponseDto<T>
        {
            Errors = errors,
            StatusCode = statusCode,
            IsSuccessful = false
        };
    }
    public static CustomResponseDto<T> Fail(List<string> errors)
    {
        return new CustomResponseDto<T>
        {
            Errors = errors,
            IsSuccessful = false
        };
    }
    public static CustomResponseDto<T> Fail(string error, int statusCode)
    {
        return new CustomResponseDto<T> { Errors = new List<string>() { error }, StatusCode = statusCode, IsSuccessful = false };
    }
}
