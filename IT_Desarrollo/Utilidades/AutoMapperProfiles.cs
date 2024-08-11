using AutoMapper;
using IT_Desarrollo_Back.DTOs;
using IT_Desarrollo_Back.Entidades;

namespace IT_Desarrollo_Back.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UsuarioRegistroDTO, Usuario>()
                .ForMember(dest => dest.Rol, opt => opt.Ignore())
                .ForMember(dest => dest.RolId, opt => opt.MapFrom(src => src.RolId));
        }
    }
}
