using BlazorScheduler;
using HR_LeaveManagement.BlazorUI.Contracts;
using HR_LeaveManagement.BlazorUI.Models;
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

    [CascadingParameter] public Task<AuthenticationState> AuthTask { get; set; }

    private System.Security.Claims.ClaimsPrincipal user;

    List<AppointmentDto> _appointments = new() 
    {
        new AppointmentDto { Title = "Congé - Christina", Start = new DateTime(2024, 7, 1), End = new DateTime(2024, 7, 12), Color = "green" },
        new AppointmentDto { Title = "Férié", Start = DateTime.Today.AddDays(4), End = DateTime.Today.AddDays(4), Color = "pink" },
        new AppointmentDto { Title = "Congé - Christina", Start = DateTime.Today.AddDays(1), End = DateTime.Today.AddDays(1), Color = "green" },
        new AppointmentDto { Title = "Congé - Damon", Start = DateTime.Today.AddDays(1), End = DateTime.Today.AddDays(1), Color = "green" },
        new AppointmentDto { Title = "Récup - Bruno", Start = DateTime.Today.AddDays(1), End = DateTime.Today.AddDays(1), Color = "orange" },
        new AppointmentDto { Title = "Récup - Laurent", Start = DateTime.Today.AddDays(1), End = DateTime.Today.AddDays(1), Color = "orange" },
        new AppointmentDto { Title = "Congé - Christophe", Start = DateTime.Today.AddDays(1), End = DateTime.Today.AddDays(1), Color = "green" },
        new AppointmentDto { Title = "Congé - Phillipe", Start = DateTime.Today.AddDays(1), End = DateTime.Today.AddDays(1), Color = "green" },
        new AppointmentDto { Title = "Récup - Geoffrey", Start = DateTime.Today.AddDays(1), End = DateTime.Today.AddDays(1), Color = "orange" },
    };

    protected async override Task OnInitializedAsync()
    {
        var authState = await AuthTask;
        this.user = authState.User;
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