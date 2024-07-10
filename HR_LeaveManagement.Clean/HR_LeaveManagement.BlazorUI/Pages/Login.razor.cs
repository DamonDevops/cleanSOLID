using HR_LeaveManagement.BlazorUI.Contracts;
using HR_LeaveManagement.BlazorUI.Models;
using Microsoft.AspNetCore.Components;

namespace HR_LeaveManagement.BlazorUI.Pages;

public partial class Login
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    [Inject]
    private IAuthService AuthService { get; set; }

    public LoginVM Model { get; set; }
    public string Message { get; set; }

    public Login()
    {

    }

    protected override void OnInitialized()
    {
        Model = new LoginVM();
    }
    protected async Task HandleLogin()
    {
        if (await AuthService.AuthenticateAsync(Model.Email, Model.Password))
        {
            NavigationManager.NavigateTo("/");
        }
        Message = "Username/Password combination unknown";
    }
}