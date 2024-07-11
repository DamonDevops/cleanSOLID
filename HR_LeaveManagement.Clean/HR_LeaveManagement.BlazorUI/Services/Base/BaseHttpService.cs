using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace HR_LeaveManagement.BlazorUI.Services.Base;

public class BaseHttpService
{
    protected IClient _client;
    protected readonly ILocalStorageService _localStorageService;

    public BaseHttpService(IClient client, ILocalStorageService localStorageService)
    {
        _client = client;
        _localStorageService = localStorageService;
    }

    protected Response<Guid> ConvertApiExceptions<Guid>(ApiException e)
    {
        if(e.StatusCode == 400)
        {
            return new Response<Guid>() { Message = "Invalid Data was submitted", ValidationErrors = e.Response, Success = false };
        }
        else if(e.StatusCode == 404)
        {
            return new Response<Guid>() { Message = "The record was not found", Success = false };
        }
        else
        {
            return new Response<Guid>() { Message = "Something went wrong, please retry later", Success = false };
        }
    }

    protected async Task AddBearerToken()
    {
        if (await _localStorageService.ContainKeyAsync("token"))
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", await _localStorageService.GetItemAsync<string>("token"));
            Console.WriteLine($"Token is: {await _localStorageService.GetItemAsync<string>("token")}");
        }
        Console.WriteLine("no token?");
    }
}