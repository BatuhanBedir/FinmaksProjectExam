using System.ComponentModel;

namespace ExamProject.Web.Models.ViewModels;

public record LoginVm
{
    [DisplayName("User Name: ")]
    public string UserName { get; set; } = null!;

    [DisplayName("Password: ")]
    public string Password { get; set; } = null!;
}
