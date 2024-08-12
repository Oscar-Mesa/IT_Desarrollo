using IT_Desarrollo_Front.Models;
using IT_Desarrollo_Front.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IT_Desarrollo_Front.Controllers
{
    public class LoginController : Controller
    {
        private readonly IServicio_API _servicio_API;

        public LoginController(IServicio_API servicio_API)
        {
            _servicio_API = servicio_API;
        }
        public async Task<IActionResult> Login(Login login)
        {

            string jsonData = JsonConvert.SerializeObject(login);
            LoginResponse respuesta = await _servicio_API.PostLogin(jsonData);

            if (respuesta.rol != null && respuesta.rol.Equals("administrador"))
            {
                return RedirectToAction("PanelAdministrador", "Login");
            }
            return View();
        }

        public async Task<IActionResult> PanelAdministrador()
        {
            return View();
        }
    }
}
