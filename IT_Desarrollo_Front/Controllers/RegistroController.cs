using IT_Desarrollo_Front.Models;
using IT_Desarrollo_Front.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IT_Desarrollo_Front.Controllers
{
    public class RegistroController : Controller
    {
        private readonly IServicio_API _servicio_API;
        LoginResponse respuesta;

        public RegistroController(IServicio_API servicio_API)
        {
            _servicio_API = servicio_API;
        }
        //Con esta función tomo la imagen del formulario y la paso a Array que recibe la API
        public async Task ImageToArray(RegistroPreguntas registroPreguntas)
        {
            if (registroPreguntas.registro.imagenFile != null)
            {
                using (var ms = new MemoryStream())
                {
                    await registroPreguntas.registro.imagenFile.CopyToAsync(ms);
                    registroPreguntas.registro.img = ms.ToArray();
                }

            }
        }

        [HttpGet]
        public async Task<IActionResult> Registro()
        {
            List<Preguntas> preguntas = await _servicio_API.GetPreguntas();

            RegistroPreguntas registroPreguntas = new RegistroPreguntas
            {
                registro = new Registro(),
                Preguntas = preguntas
            };

            // Guardo en temp para pasar al post y que no se pierda el valor de los campos generados dinamicamente
            TempData["RegistroPreguntas"] = JsonConvert.SerializeObject(registroPreguntas);

            return View(registroPreguntas);
        }

        [HttpPost]
        public async Task<IActionResult> Registro(RegistroPreguntas registroPreguntas)
        {
           
            if (TempData["RegistroPreguntas"] != null)
            {
                var registroPreguntasJson = TempData["RegistroPreguntas"].ToString();
                var tempRegistroPreguntas = JsonConvert.DeserializeObject<RegistroPreguntas>(registroPreguntasJson);
                registroPreguntas.Preguntas = tempRegistroPreguntas.Preguntas;
               
            }
            registroPreguntas.registro.rolId = 1;
            await ImageToArray(registroPreguntas);
            string jsonData = JsonConvert.SerializeObject(registroPreguntas.registro);


            respuesta = await _servicio_API.PostRegistro(jsonData);

            if (respuesta.mensaje.Equals($"Usuario {respuesta.usuario.nombre} registrado exitosamente.") ||
                respuesta.mensaje.Equals($"Ya existe un usuario registrado con el correo {respuesta.usuario.email}."))
            {
                return RedirectToAction("Registro");
            }




            return View(registroPreguntas);

        }

    }
}
