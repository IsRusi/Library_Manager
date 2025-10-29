using AutoMapper;
using Library_Manager.Application.DTO;
using Library_Manager.Domain.Models;

namespace Library_Manager.Infrastructure.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorDTO>();
            CreateMap<AuthorDTO, Author>();
            CreateMap<CreateAuthorDTO, Author>();
            CreateMap<CreateAuthorDTO, AuthorDTO>();
            CreateMap<UpdateAuthorDTO, Author>();
        }
    }
}