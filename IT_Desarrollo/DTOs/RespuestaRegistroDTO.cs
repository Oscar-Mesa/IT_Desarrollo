using System.ComponentModel.DataAnnotations;

namespace IT_Desarrollo_Back.DTOs
{
    public class RespuestaRegistroDTO
    {
        [StringLength(maximumLength:100)]
        public string ?pregunta { get; set; }
        [StringLength(maximumLength: 100)]
        public string respuesta { get; set; }
        public int PreguntaId { get; set; }
    }
}
