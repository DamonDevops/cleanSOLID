using HR_LeaveManagement.BlazorUI.Contracts;
using HR_LeaveManagement.BlazorUI.Models;
using Microsoft.AspNetCore.Components;

namespace HR_LeaveManagement.BlazorUI.Pages;

public partial class Register
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    [Inject]
    private IAuthService AuthService { get; set; }

    public RegisterVM Model { get; set; }
    public string Message { get; set; }

    protected override void OnInitialized()
    {
        Model = new RegisterVM();
    }
    protected async Task HandleRegister() 
    {
        var result = await AuthService.RegisterAsync(Model.FirstName, Model.LastName, Model.Username, Model.Email, Model.Password);
        if (result)
        {
            NavigationManager.NavigateTo("/");
        }
        Message = "Something went wrong, please try again";
    }
}