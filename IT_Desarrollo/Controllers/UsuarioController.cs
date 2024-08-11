using AutoMapper;
using GusticosWebAPI;
using IT_Desarrollo_Back.DTOs;
using IT_Desarrollo_Back.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IT_Desarrollo_Back.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public UsuarioController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        private Object SetRespuesta(string mensaje, Usuario usuario)
        {
            var respuesta = (mensaje, usuario);

            return respuesta;
        }

        [HttpPost]
        public async Task<ActionResult> PostRegistro(UsuarioRegistroDTO usuarioRegistroDTO)
        {
            var emailExistente = await context.tbl_usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.email == usuarioRegistroDTO.email);

            if (emailExistente != null)
            {
                return BadRequest(SetRespuesta($"Ya existe un usuario registrado con el correo {emailExistente.email}.",
                    emailExistente));
            }

            var usuario = mapper.Map<Usuario>(usuarioRegistroDTO);

            context.tbl_usuarios.Add(usuario);
            await context.SaveChangesAsync();

            return Ok(SetRespuesta($"Usuario {usuario.nombre} registrado exitosamente.",
                usuario));
        }
    }
}
