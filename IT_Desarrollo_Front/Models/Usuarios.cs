using static System.Net.Mime.MediaTypeNames;

namespace IT_Desarrollo_Front.Models
{
    public class Usuarios
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public int codigo_pais { get; set; }
        public string telefono { get; set; }
        public byte[] img { get; set; }
        public string contrasena { get; set; }
        public string descripcion { get; set; }
        public List<Respuestas> Respuestas { get; set; }

        public string ImagenBase64
        {
            get
            {
                return img != null ? Convert.ToBase64String(img) : string.Empty;
            }
        }
    }
}
