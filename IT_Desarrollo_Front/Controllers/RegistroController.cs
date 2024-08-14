using IT_Desarrollo_Front.Models;
using IT_Desarrollo_Front.Services;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;

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

            if (registroPreguntas.registro.respuestas != null)
            {
                registroPreguntas.registro.respuestas = registroPreguntas.registro.respuestas
                    .Where(r => !string.IsNullOrEmpty(r.respuesta))
                    .ToList();
            }

            // Filtrar propiedades nulas del registro
            var DictNoNulos = new Dictionary<string, object>();
            var propiedades = registroPreguntas.registro.GetType().GetProperties();

            foreach (var prop in propiedades)
            {
                var value = prop.GetValue(registroPreguntas.registro);

                if (value != null && !(value is string str && string.IsNullOrEmpty(str)))
                {
                    DictNoNulos.Add(prop.Name, value);
                }
            }

            string jsonData = JsonConvert.SerializeObject(DictNoNulos);


            respuesta = await _servicio_API.PostRegistro(jsonData);

            if (respuesta.mensaje.Equals($"Ya existe un usuario registrado con el correo {respuesta.usuario.email}."))
            {
                ViewBag.CorreoExistente = respuesta.mensaje;
                TempData["RegistroPreguntas"] = JsonConvert.SerializeObject(registroPreguntas);
                return View(registroPreguntas);
            }

            try
            {
                if (respuesta.mensaje.Equals($"Usuario {respuesta.usuario.nombre} registrado exitosamente."))
                {
                    await EnviarCorreoDeAgradecimiento(respuesta.usuario.email, respuesta.usuario.nombre);
                    //TempData["RegistroPreguntas"] = JsonConvert.SerializeObject(registroPreguntas);
                    return RedirectToAction("Exitoso");
                }
            }
            catch
            {
                return View(registroPreguntas);
            }

            return View(registroPreguntas);

        }

        private async Task EnviarCorreoDeAgradecimiento(string email, string nombre)
        {

            string mymail = Environment.GetEnvironmentVariable("EMAIL_ENV");
            string contrasena = Environment.GetEnvironmentVariable("EMAIL_PASSWORD_ENV");

            // Validar que las variables de entorno no sean nulas o vacías
            if (string.IsNullOrEmpty(mymail) || string.IsNullOrEmpty(contrasena))
            {
                throw new InvalidOperationException("El correo o la contraseña no están configurados en las variables de entorno.");
            }

            var remitente = new MailAddress(mymail);
            var destinatario = new MailAddress(email);

            string subject = "Gracias por registrarte en WePlot";

            string body = $"Hola {nombre},\n\nGracias por registrarte en WePlot.\n\n" +
                          $"¡Esperamos que disfrutes nuestra plataforma!\n\nSaludos,\n" +
                          $"El equipo de WePlot";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,  // Asegúrate de que esto sea false
                Credentials = new NetworkCredential(mymail, contrasena)  // Las credenciales del remitente
            };

            using (var message = new MailMessage(remitente, destinatario)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            })
            {
                await smtp.SendMailAsync(message);
            }
        }

        public IActionResult Exitoso()
        {
            return View();
        }

    }
    }
