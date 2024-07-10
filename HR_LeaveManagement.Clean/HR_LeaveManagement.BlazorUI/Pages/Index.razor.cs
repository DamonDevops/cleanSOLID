using HR_LeaveManagement.BlazorUI.Contracts;
using HR_LeaveManagement.BlazorUI.Providers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace HR_LeaveManagement.BlazorUI.Pages;

public partial class Index
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    [Inject]
    public IAuthService AuthService { get; set; }
    [Inject]
    private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    protected async override Task OnInitializedAsync()
    {
        await ((ApiAuthenticationStateProvider)AuthenticationStateProvider).GetAuthenticationStateAsync();
    }
    protected void GoToLogin() 
    {
        NavigationManager.NavigateTo("login/");
    }
    protected void GoToRegister() 
    {
        NavigationManager.NavigateTo("register/");
    }
    protected async void Logout() 
    {
        await AuthService.Logout();
    }
}