using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Services;

public class AuthorService : BaseHttpService, IAuthorService
{
    private readonly IClient _client;
    
    public AuthorService(IClient client, ILocalStorageService localStorageService) : base(client, localStorageService)
    {
        _client = client;
    }

    public async Task<Response<List<AuthorReadOnlyDto>>> GetAuthors()
    {
        Response<List<AuthorReadOnlyDto>> response;

        try
        {
            await GetBearerToken();
            var data = await _client.AuthorsAllAsync();
            response = new Response<List<AuthorReadOnlyDto>>
            {
                Data = data.ToList(),
                IsSuccess = true
            };
        }
        catch (ApiException exception)
        {
            response = ConvertApiExceptions<List<AuthorReadOnlyDto>>(exception);
        }

        return response;
    }

    public async Task<Response<int>> CreateAuthor(AuthorCreateDto author)
    {
        Response<int> response = new();
        try
        {
            await GetBearerToken();
            await _client.AuthorsPOSTAsync(author);
        }
        catch (ApiException exception)
        {
            response = ConvertApiExceptions<int>(exception);
        }

        return response;
    }
}