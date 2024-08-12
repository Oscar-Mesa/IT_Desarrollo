using Microsoft.AspNetCore.Identity;

namespace IT_Desarrollo_Back.Entidades
{
    public class Usuario
    {
        public int pkid { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public int codigo_pais { get; set; }
        public int telefono { get; set; }
        public byte[] img { get; set; }
        public string contrasena { get; set; }
        public int RolId { get; set; }  
        public Rol Rol { get; set; }

    }
}
