namespace IT_Desarrollo_Front.Models
{
    public class Registro
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public int codigo_pais { get; set; }
        public int telefono { get; set; }
        public byte[] img { get; set; }
        public string contrasena { get; set; }
        public int rolId { get; set; }
        public List<Respuestas> respuestas { get; set; }
    }
}
