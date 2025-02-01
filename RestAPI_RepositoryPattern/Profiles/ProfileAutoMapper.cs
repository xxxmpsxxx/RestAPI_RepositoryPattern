using AutoMapper;
using RestAPI_RepositoryPattern.Dto;
using RestAPI_RepositoryPattern.Models;

namespace RestAPI_RepositoryPattern.Profiles
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {
            CreateMap<Usuario, UsuarioListaDto>();
        }
    }
}
