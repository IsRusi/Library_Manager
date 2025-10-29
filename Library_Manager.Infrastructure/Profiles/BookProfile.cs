using AutoMapper;
using Library_Manager.Application.DTO;
using Library_Manager.Domain.Models;

namespace Library_Manager.Infrastructure.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDTO>();
            CreateMap<BookDTO, Book>();
            CreateMap<CreateBookDTO, Book>();
            CreateMap<CreateBookDTO, BookDTO>();
            CreateMap<UpdateBookDTO, Book>();
            CreateMap<Book, BookWithAuthorDTO>();
        }
    }
}