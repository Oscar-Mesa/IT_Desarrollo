using AutoMapper;
using GusticosWebAPI;
using IT_Desarrollo_Back.DTOs;
using IT_Desarrollo_Back.Entidades;
using IT_Desarrollo_Back.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace IT_Desarrollo_Back.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IPasswordHasher<Usuario> passwordHasher;

        private readonly IConfiguration configuration;


        public UsuarioController(ApplicationDbContext context, IConfiguration configuration, IMapper mapper, IPasswordHasher<Usuario> passwordHasher)
        {
            this.context = context;
            this.mapper = mapper;
            this.passwordHasher = passwordHasher;
            this.configuration = configuration;
        }

        private RespuestaApi SetRespuesta(string mensaje, Usuario usuario)
        {
            return new RespuestaApi(mensaje, usuario);
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
        public async Task<IActionResult> PostLogin(UsuarioLoginDTO loginDTO)
        {
            var usuario = await context.tbl_usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.email == loginDTO.email);

            if (usuario == null)
            {
                return Unauthorized(SetRespuesta("El usuario o la contraseña son incorrectos.", null));
            }

            var resultado = passwordHasher.VerifyHashedPassword(usuario, usuario.contrasena, loginDTO.contrasena);

            if (resultado == PasswordVerificationResult.Failed)
            {
                return Unauthorized(SetRespuesta("El usuario o la contraseña son incorrectos.", null));
            }

            string token = CrearToken(usuario);

            return Ok(SetRespuesta($"bearer {token}", usuario));
        }

        [HttpGet, Authorize(Roles = "administrador")]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await context.tbl_usuarios
             .Include(u => u.Rol)
             .Select(u => new
             {
                 Usuario = u,
                 Respuestas = context.tbl_respuestas.Where(r => r.UsuarioId == u.pkid).ToList()
             })
            .ToListAsync();

            //var usuariosDTO = mapper.Map<List<UsuarioDTO>>(usuarios);

            //Tuve que crear un DTO sin mapper porque me genera conflictos 
            var usuariosDTO = usuarios.Select(u => new
            {
                u.Usuario.nombre,
                u.Usuario.apellido,
                u.Usuario.email,
                u.Usuario.codigo_pais,
                u.Usuario.telefono,
                u.Usuario.pais,
                u.Usuario.img,
                u.Usuario.contrasena,
                u.Usuario.Rol.descripcion,
                Respuestas = u.Respuestas.Select(r => new
                {
                    r.pregunta,
                    r.respuesta
                }).ToList()
            }).ToList();

            return Ok(usuariosDTO);
        }

        [HttpGet("preguntas")]
        public async Task<IActionResult> GetPreguntas()
        {
            var pregunta = await context.tbl_preguntas
                .Select(p => new
                {
                    Pregunta = p
                }).ToListAsync();

            return Ok(pregunta);
        }

        private string CrearToken(Usuario usuario)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.nombre),
                new Claim(ClaimTypes.Role, usuario.Rol.descripcion)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}
