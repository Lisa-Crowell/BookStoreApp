using AutoMapper;
using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Services;

public class AuthorService : BaseHttpService, IAuthorService
{
    private readonly IClient _client;
    private readonly IMapper _mapper;
    public AuthorService(IClient client, ILocalStorageService localStorageService, IMapper mapper) : base(client, localStorageService)
    {
        _client = client;
        _mapper = mapper;
    }

    public async Task<Response<List<AuthorReadOnlyDto>>> GetAuthor()
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

    public async Task<Response<AuthorReadOnlyDto>> GetAuthor(int id)
    {
        Response<AuthorReadOnlyDto> response;

        try
        {
            await GetBearerToken();
            var data = await _client.AuthorsGETAsync(id);
            response = new Response<AuthorReadOnlyDto>
            {
                Data = data,
                IsSuccess = true
            };
        }
        catch (ApiException exception)
        {
            response = ConvertApiExceptions<AuthorReadOnlyDto>(exception);
        }

        return response;
    }

    public async Task<Response<AuthorUpdateDto>> GetAuthorForUpdate(int id)
    {
        Response<AuthorUpdateDto> response;

        try
        {
            await GetBearerToken();
            var data = await _client.AuthorsGETAsync(id);
            response = new Response<AuthorUpdateDto>
            {
                Data = _mapper.Map<AuthorUpdateDto>(data),
                IsSuccess = true
            };
        }
        catch (ApiException exception)
        {
            response = ConvertApiExceptions<AuthorUpdateDto>(exception);
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

    public async Task<Response<int>> EditAuthor(int id, AuthorUpdateDto author)
    {
        Response<int> response = new();

        try
        {
            await GetBearerToken();
            await _client.AuthorsPUTAsync(id, author);
        }
        catch (ApiException exception)
        {
            response = ConvertApiExceptions<int>(exception);
        }

        return response;
    }
}