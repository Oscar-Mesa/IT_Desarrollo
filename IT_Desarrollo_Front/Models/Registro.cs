﻿using static System.Net.Mime.MediaTypeNames;

namespace IT_Desarrollo_Front.Models
{
    public class Registro
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public int codigo_pais { get; set; }
        public string telefono { get; set; }
        public string pais { get; set; }
        public byte[] img { get; set; }
        public string contrasena { get; set; }
        public int rolId { get; set; }
        public List<Respuestas> respuestas { get; set; }


        //obtener la imagen del formulario para después transformarla en bytes dentro del controlador
        public IFormFile? imagenFile { get; set; }

        //convertir la base64 a string
        public string ImagenBase64
        {
            get
            {
                return img != null ? Convert.ToBase64String(img) : string.Empty;
            }
           
        }
    }
}
