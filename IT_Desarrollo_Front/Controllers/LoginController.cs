using IT_Desarrollo_Front.Models;
using IT_Desarrollo_Front.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace IT_Desarrollo_Front.Controllers
{
    public class LoginController : Controller
    {
        private readonly IServicio_API _servicio_API;

        LoginResponse respuesta;
        PerfilResponse respuestaPerfil;
        public LoginController(IServicio_API servicio_API)
        {
            _servicio_API = servicio_API;

        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookie_authentication");
            return RedirectToAction("Login", "Login");
        }

        //para solo visualizar la vista sin consumir API
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //se llama cuando se ejecuta el formulario de inicio de sesión
        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {

            string jsonData = JsonConvert.SerializeObject(login);
            respuesta = await _servicio_API.PostLogin(jsonData);
            if (respuesta.rol == null)
            {
                ViewBag.Mensaje = "usuario o contraseña incorrectos.";
            }

            if (respuesta.rol != null)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, respuesta.usuario.pkid.ToString()),
                new Claim(ClaimTypes.Email, respuesta.usuario.email),
                new Claim(ClaimTypes.Name, respuesta.usuario.nombre),
                new Claim(ClaimTypes.Role, respuesta.rol)

            };

                var claimsIdentity = new ClaimsIdentity(claims, "Cookie_authentication");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync("Cookie_authentication", claimsPrincipal);

                if (respuesta.rol.Equals("administrador"))
                {
                    //guardo el token para usarlo en la otra vista 
                    //solo guardo el token porque tratar con arreglo de bytes en TemData genera problemas
                    TempData["Respuesta"] = JsonConvert.SerializeObject(respuesta.mensaje);
                    return RedirectToAction("PanelAdministrador", "Login");
                }

                if (respuesta.rol.Equals("usuario"))
                {
                    TempData["Respuesta"] = JsonConvert.SerializeObject(respuesta.mensaje);
                    return RedirectToAction("PanelUsuario", "Login");
                }

            }

            return View("Login");
        }
        [Authorize(AuthenticationSchemes = "Cookie_authentication", Roles = "usuario")]
        public async Task<IActionResult> PanelUsuario(Registro usuario)
        {
            if(usuario != null)
            {
                if (TempData["Respuesta"] != null)
                {
                    var respuestaJson = JsonConvert.DeserializeObject<string>(TempData["Respuesta"].ToString());
                    respuestaPerfil = await _servicio_API.GetPerfil(respuestaJson);
                    
                    if (respuestaPerfil.mensaje.Equals("Obtención de perfil exitosa"))
                    {
                        usuario = respuestaPerfil.usuario;

                        //almacenando el token antes de ver la vista, asegura que el usuario aunque recargue la pagina 
                        //no lo saque de su perfil
                        TempData["Respuesta"] = JsonConvert.SerializeObject(respuestaJson);
                        return View(usuario);
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Login");
                }
            }
            
           
            return View();
            
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Cookie_authentication", Roles = "administrador")]
        public async Task<IActionResult> PanelAdministrador()
        {

            if (TempData["Respuesta"] != null)
            {
                List<Preguntas> preguntas = await _servicio_API.GetPreguntas();
                ViewBag.Preguntas = preguntas;
                var respuestaJson = JsonConvert.DeserializeObject<string>(TempData["Respuesta"].ToString());
                List<Usuarios> usuarios = await _servicio_API.GetUsuarios(respuestaJson);
                TempData["Respuesta"] = JsonConvert.SerializeObject(respuestaJson);
                return View(usuarios);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Cookie_authentication", Roles = "administrador")]
        public async Task<IActionResult> PanelAdministrador(List<Preguntas> preguntas)
        {
            if (preguntas != null && preguntas.Count > 0)
            {
                // Filtrar preguntas con descripción no nula
                var preguntasValidas = preguntas.Where(p => !string.IsNullOrEmpty(p.Pregunta.descripcion)).ToList();

                if (preguntasValidas.Count > 0)
                {
                    string jsonData = JsonConvert.SerializeObject(preguntasValidas);
                    var respuestaJson = JsonConvert.DeserializeObject<string>(TempData["Respuesta"].ToString());
                    await _servicio_API.PutPreguntas(jsonData, respuestaJson);
                    TempData["SuccessMessage"] = "Preguntas actualizadas exitosamente.";
                }
                else
                {
                    TempData["ErrorMessage"] = "No se recibieron preguntas válidas para actualizar.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "No se recibieron preguntas para actualizar.";
            }

            return RedirectToAction("PanelAdministrador");
        }

        [Route("AccesoDenegado")]
        public async Task<IActionResult> AccesoDenegado()
        {
            return View();
        }
    }
}
