namespace IT_Desarrollo_Back.Entidades
{
    public class Respuesta
    {
        public int pkid { get; set; }
        public string pregunta { get; set; }
        public string respuesta { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int PreguntaId { get; set; }
        public Pregunta Pregunta { get; set; }
    }
}
