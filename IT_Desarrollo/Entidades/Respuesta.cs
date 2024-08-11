namespace IT_Desarrollo_Back.Entidades
{
    public class Respuesta
    {
        public int pkid { get; set; }
        public string pregunta { get; set; }
        public string respuesta { get; set; }
        public Usuario usuarioid { get; set; }
        public Pregunta preguntaid { get; set; }
    }
}
