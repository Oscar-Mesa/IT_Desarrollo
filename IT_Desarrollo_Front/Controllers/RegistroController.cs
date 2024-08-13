using IT_Desarrollo_Front.Models;
using IT_Desarrollo_Front.Services;
using Microsoft.AspNetCore.Mvc;

namespace IT_Desarrollo_Front.Controllers
{
    public class RegistroController : Controller
    {
        private readonly IServicio_API _servicio_API;

        public RegistroController(IServicio_API servicio_API)
        {
            _servicio_API = servicio_API;
        }
        public async Task <IActionResult> Registro()
        {
            List<Preguntas> preguntas = await _servicio_API.GetPreguntas();

            var registroPreguntas = new RegistroPreguntas
            {
                Registro = new Registro(),
                Preguntas = preguntas
            };

            return View(registroPreguntas);
        }
    }
}
