using AutoMapper;
using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Configurations;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<AuthorReadOnlyDto, AuthorUpdateDto>().ReverseMap();
    }
}