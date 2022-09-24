using AutoMapper;
using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Services;

public class BookService : BaseHttpService, IBookService
{
    private readonly IClient _client;
    private readonly IMapper _mapper;

    public BookService(IClient client, ILocalStorageService localStorage, IMapper mapper) : base(client, localStorage)
    {
        _client = client;
        _mapper = mapper;
    }
    
    public async Task<Response<int>> CreateBook(BookCreateDto book)
    {
        Response<int> response = new();

        try
        {
            await GetBearerToken();
            await _client.BooksPOSTAsync(book);
        }
        catch (ApiException exception)
        {
            response = ConvertApiExceptions<int>(exception);
        }

        return response; 
    }

    public async Task<Response<int>> DeleteBook(int id)
    {
        Response<int> response = new();

        try
        {
            await GetBearerToken();
            await _client.BooksDELETEAsync(id);
        }
        catch (ApiException exception)
        {
            response = ConvertApiExceptions<int>(exception);
        }

        return response;
    }

    public async Task<Response<int>> EditBook(int id, BookUpdateDto book)
    {
        Response<int> response = new();

        try
        {
            await GetBearerToken();
            await _client.BooksPUTAsync(id, book);
        }
        catch (ApiException exception)
        {
            response = ConvertApiExceptions<int>(exception);
        }

        return response;
    }

    public async Task<Response<BookDetailsDto>> GetBook(int id)
    {
        Response<BookDetailsDto> response;

        try
        {
            await GetBearerToken();
            var data = await _client.BooksGETAsync(id);
            response = new Response<BookDetailsDto>
            {
                Data = data,
                IsSuccess = true
            };
        }
        catch (ApiException exception)
        {
            response = ConvertApiExceptions<BookDetailsDto>(exception);
        }

        return response;
    }

    public async Task<Response<List<BookReadOnlyDto>>> GetBook()
    {
        Response<List<BookReadOnlyDto>> response;

        try
        {
            await GetBearerToken();
            var data = await _client.BooksAllAsync();
            response = new Response<List<BookReadOnlyDto>>
            {
                Data = data.ToList(),
                IsSuccess = true
            };
        }
        catch (ApiException exception)
        {
            response = ConvertApiExceptions<List<BookReadOnlyDto>>(exception);
        }

        return response;
    }

    public async Task<Response<BookUpdateDto>> GetBookForUpdate(int id)
    {
        Response<BookUpdateDto> response;

        try
        {
            await GetBearerToken();
            var data = await _client.BooksGETAsync(id);
            response = new Response<BookUpdateDto>
            {
                Data = _mapper.Map<BookUpdateDto>(data),
                IsSuccess = true
            };
        }
        catch (ApiException exception)
        {
            response = ConvertApiExceptions<BookUpdateDto>(exception);
        }

        return response;
    }
}