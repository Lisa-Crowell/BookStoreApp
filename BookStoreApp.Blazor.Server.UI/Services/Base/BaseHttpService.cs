using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace BookStoreApp.Blazor.Server.UI.Services.Base;

public class BaseHttpService
{
    public readonly IClient _client;
    public readonly ILocalStorageService _localStorage;
    public BaseHttpService(IClient client, ILocalStorageService localStorage)
    {
        _client = client;
        _localStorage = localStorage;
    }

    protected Response<Guid> ConvertApiExceptions<Guid>(ApiException apiException)
    {
        if (apiException.StatusCode == 400)
        {
            return new Response<Guid>() { Message = "Validation errors have occurred", ValidationErrors = apiException.Response, IsSuccess = false };
        }
        if (apiException.StatusCode == 404)
        {
            return new Response<Guid>() { Message = "The requested item could not be found.", IsSuccess = false };
        }
        if (apiException.StatusCode is >= 200 and <= 299)
        {
            return new Response<Guid>() { Message = "Operations reported success", IsSuccess = true };
        }
        return new Response<Guid>() { Message = "Something went wrong, please try again later.", IsSuccess = false };
    }

    protected async Task GetBearerToken()
    {
        var token = await _localStorage.GetItemAsync<string>("accessToken");
        if (token != null)
        {
            _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}