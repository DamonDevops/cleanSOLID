using Blazored.LocalStorage;
using HR_LeaveManagement.BlazorUI.Contracts;
using HR_LeaveManagement.BlazorUI.Providers;
using HR_LeaveManagement.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace HR_LeaveManagement.BlazorUI.Services;

public class AuthService : BaseHttpService, IAuthService
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public AuthService(IClient client, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider) : base(client, localStorageService)
    {
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<bool> AuthenticateAsync(string email, string password)
    {
        try
        {
            AuthRequest authRequest = new AuthRequest() { Email = email, Password = password };
            var authResponse = await _client.LoginAsync(authRequest);
            if (authResponse.Token != string.Empty)
            {
                await _localStorageService.SetItemAsync("token", authResponse.Token);
                await ((ApiAuthenticationStateProvider) _authenticationStateProvider).LoggedIn();

                return true;
            }
            return false;
        }
        catch(Exception e)
        {
            return false;
        }
        
    }

    public async Task Logout()
    {
        await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedOut();
    }

    public async Task<bool> RegisterAsync(string firstname, string lastname, string username, string email, string password)
    {
        try
        {
            RegistrationRequest registrationRequest = new RegistrationRequest() { FirstName = firstname, LastName = lastname, Username = username, Email = email, Password = password };
            var registerResponse = await _client.RegisterAsync(registrationRequest);
            if (!string.IsNullOrEmpty(registerResponse.UserId))
            {
                return true;
            }
            return false;
        }catch(Exception e)
        {
            return false;
        }
    }
}
