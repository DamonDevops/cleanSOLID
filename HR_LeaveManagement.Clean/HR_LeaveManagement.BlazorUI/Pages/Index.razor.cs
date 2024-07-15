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

    List<AppointmentDto> _appointments = new() 
    {
        new AppointmentDto { Title = "Open Sourced Date", Start = new DateTime(2021, 7, 9), End = new DateTime(2021, 7, 9), Color = "green" },
        new AppointmentDto { Title = "Vacation", Start = DateTime.Today.AddDays(4), End = DateTime.Today.AddDays(14), Color = "pink" },
        new AppointmentDto { Title = "Really busy day 1", Start = DateTime.Today.AddDays(1), End = DateTime.Today.AddDays(1), Color = "orange" },
        new AppointmentDto { Title = "Really busy day 2", Start = DateTime.Today.AddDays(1), End = DateTime.Today.AddDays(1), Color = "orange" },
        new AppointmentDto { Title = "Really busy day 3", Start = DateTime.Today.AddDays(1), End = DateTime.Today.AddDays(1), Color = "orange" },
        new AppointmentDto { Title = "Really busy day 4", Start = DateTime.Today.AddDays(1), End = DateTime.Today.AddDays(1), Color = "orange" },
        new AppointmentDto { Title = "Really busy day 5", Start = DateTime.Today.AddDays(1), End = DateTime.Today.AddDays(1), Color = "orange" },
        new AppointmentDto { Title = "Really busy day 6", Start = DateTime.Today.AddDays(1), End = DateTime.Today.AddDays(1), Color = "orange" },
        new AppointmentDto { Title = "Really busy day 7", Start = DateTime.Today.AddDays(1), End = DateTime.Today.AddDays(1), Color = "orange" },
    };

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