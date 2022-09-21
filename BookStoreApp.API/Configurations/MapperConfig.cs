using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Author;
using BookStoreApp.API.Models.Book;
using BookStoreApp.API.Models.User;

namespace BookStoreApp.API.Configurations;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<AuthorCreateDto, Author>().ReverseMap();
        CreateMap<AuthorUpdateDto, Author>().ReverseMap();
        CreateMap<AuthorDetailsDto, Author>().ReverseMap();
        CreateMap<AuthorReadOnlyDto, Author>().ReverseMap();
        
        CreateMap<BookCreateDto, Book>().ReverseMap();
        CreateMap<BookUpdateDto, Book>().ReverseMap();
        CreateMap<Book, BookReadOnlyDto>().ForMember(
            b => b.AuthorName, 
            d => d.MapFrom(
                map => $"{map.Author.FirstName} {map.Author.LastName}")).ReverseMap();
        CreateMap<Book, BookDetailsDto>().ForMember(
            b => b.AuthorName, 
            d => d.MapFrom(
                map => $"{map.Author.FirstName} {map.Author.LastName}")).ReverseMap();

        CreateMap<ApiUser, UserDto>().ReverseMap();
    }
}