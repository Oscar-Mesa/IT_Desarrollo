namespace IT_Desarrollo_Back.DTOs
{
    internal class UsuarioDTO
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public int codigo_pais { get; set; }
        public string telefono { get; set; }
        public string pais { get; set; }
        public string imgBase64 { get; set; }
        public string Rol { get; set; }
        public RespuestaDTO respuesta { get; set; }
    }
}