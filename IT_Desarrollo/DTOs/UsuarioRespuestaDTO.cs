namespace IT_Desarrollo_Back.DTOs
{
    public class UsuarioRespuestaDTO
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public string codigo_pais { get; set; }
        public int telefono { get; set; }
        public string pais { get; set; }
        public string rol { get; set; }
        //public byte[] img { get; set; }
        public List<RespuestaDTO> respuestas { get; set; }
    }
}
