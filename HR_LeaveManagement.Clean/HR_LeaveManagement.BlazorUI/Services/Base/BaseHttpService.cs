namespace HR_LeaveManagement.BlazorUI.Services.Base;

public class BaseHttpService
{
    protected IClient _client;

    public BaseHttpService(IClient client)
    {
        _client = client;
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
}