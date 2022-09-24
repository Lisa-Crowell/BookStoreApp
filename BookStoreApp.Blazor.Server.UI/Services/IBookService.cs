using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Services;

public interface IBookService
{
    Task<Response<List<BookReadOnlyDto>>> GetBook();
    Task<Response<BookDetailsDto>> GetBook(int id);
    Task<Response<BookUpdateDto>> GetBookForUpdate(int id);
    Task<Response<int>> CreateBook(BookCreateDto bookCreateDto);
    Task<Response<int>> EditBook(int id, BookUpdateDto bookUpdateDto);
    Task<Response<int>> DeleteBook(int id);
}