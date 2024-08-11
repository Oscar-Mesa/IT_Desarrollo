using AutoMapper;
using GusticosWebAPI;
using IT_Desarrollo_Back.DTOs;
using IT_Desarrollo_Back.Entidades;
using Microsoft.AspNetCore.Identity;
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
        private readonly IPasswordHasher<Usuario> passwordHasher;

        public UsuarioController(ApplicationDbContext context, IMapper mapper, IPasswordHasher<Usuario> passwordHasher)
        {
            this.context = context;
            this.mapper = mapper;
            this.passwordHasher = passwordHasher;
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

            usuario.contrasena = passwordHasher.HashPassword(usuario, usuarioRegistroDTO.contrasena);

            context.tbl_usuarios.Add(usuario);
            //Primero guardamos el usuario
            await context.SaveChangesAsync();

            if (usuarioRegistroDTO.Respuestas != null && usuarioRegistroDTO.Respuestas.Any())
            {
                foreach (var respuestaDTO in usuarioRegistroDTO.Respuestas)
                {
                    var respuesta = mapper.Map<Respuesta>(respuestaDTO);
                    respuesta.UsuarioId = usuario.pkid;
              
                    context.tbl_respuestas.Add(respuesta);
                }

                //Por último guardamos las preguntas personalizadas
                await context.SaveChangesAsync();
            }

            return Ok(SetRespuesta($"Usuario {usuario.nombre} registrado exitosamente.",
                usuario));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UsuarioLoginDTO loginDTO)
        {
            var usuario = await context.tbl_usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.email == loginDTO.email);

            if (usuario == null)
            {
                return Unauthorized("El usuario o la contraseña son incorrectos.");
            }

            var resultado = passwordHasher.VerifyHashedPassword(usuario, usuario.contrasena, loginDTO.contrasena);

            if (resultado == PasswordVerificationResult.Failed)
            {
                return Unauthorized("El usuario o la contraseña son incorrectos.");
            }

            return Ok("Inicio de sesión exitoso");
        }
    }
}
