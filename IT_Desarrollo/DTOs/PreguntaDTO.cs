    namespace IT_Desarrollo_Back.DTOs
{
    public class PreguntaDTO
    {
        public Pregunta Pregunta { get; set; }
    }

    public class Pregunta
    {
        public int pkid { get; set; }
        public string descripcion { get; set; }
    }
}
