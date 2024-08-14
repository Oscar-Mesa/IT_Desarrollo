namespace IT_Desarrollo_Front.Models
{
    public class Usuario
    {
        public int pkid { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public int codigo_pais { get; set; }
        public string telefono { get; set; }
        public string pais { get; set; }
        public byte[]? img { get; set; }
        public string contrasena { get; set; }
        public int RolId { get; set; }
        public Rol Rol { get; set; }

        public string ImagenBase64
        {
            get
            {
                return img != null ? Convert.ToBase64String(img) : string.Empty;
            }
        }
    }
}