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
            if(respuesta.rol == null)
            {
                ViewBag.Mensaje = "usuario o contraseña incorrectos.";
            }

            if (respuesta.rol != null)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, respuesta.usuario.nombre),
                new Claim(ClaimTypes.Role, respuesta.rol)
            };

                var claimsIdentity = new ClaimsIdentity(claims, "Cookie_authentication");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync("Cookie_authentication", claimsPrincipal);

                if (respuesta.rol.Equals("administrador"))
                {
                    //guardo el objeto respuesta para usarlo en la otra vista 
                    TempData["Respuesta"] = JsonConvert.SerializeObject(respuesta);
                    return RedirectToAction("PanelAdministrador", "Login");
                }

                if (respuesta.rol.Equals("usuario"))
                {
                    return RedirectToAction("PanelUsuario", "Login");
                }

            }

            return View("Login");
        }
        [Authorize(AuthenticationSchemes = "Cookie_authentication", Roles = "usuario")]
        public async Task<IActionResult> PanelUsuario()
        {
            return View();
        }

        [Authorize(AuthenticationSchemes = "Cookie_authentication", Roles = "administrador")]
        public async Task<IActionResult> PanelAdministrador()
        {

            if (TempData["Respuesta"] != null)
            {
                respuesta = JsonConvert.DeserializeObject<LoginResponse>(TempData["Respuesta"].ToString());
                List<Usuarios> usuarios = await _servicio_API.GetUsuarios(respuesta.mensaje);
                return View(usuarios);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

            return View();
        }
        [Route("AccesoDenegado")]
        public async Task<IActionResult> AccesoDenegado()
        {
            return View();
        }
    }
}
