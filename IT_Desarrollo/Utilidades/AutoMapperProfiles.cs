using AutoMapper;
using IT_Desarrollo_Back.DTOs;
using IT_Desarrollo_Back.Entidades;

namespace IT_Desarrollo_Back.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<Usuario, UsuarioDTO>()
            .ForMember(dest => dest.Rol, opt => opt.MapFrom(src => src.Rol.descripcion));

            CreateMap<UsuarioRegistroDTO, Usuario>()
                .ForMember(dest => dest.Rol, opt => opt.Ignore())
                .ForMember(dest => dest.RolId, opt => opt.MapFrom(src => src.RolId));

            CreateMap<RespuestaRegistroDTO, Respuesta>()
             .ForMember(dest => dest.pregunta, opt => opt.MapFrom(src => src.pregunta))  
             .ForMember(dest => dest.respuesta, opt => opt.MapFrom(src => src.respuesta))
             .ForMember(dest => dest.PreguntaId, opt => opt.MapFrom(src => src.PreguntaId))
             .ForMember(dest => dest.Pregunta, opt => opt.Ignore());

            CreateMap<PreguntaDTO, Pregunta>();
        }
    }
}
