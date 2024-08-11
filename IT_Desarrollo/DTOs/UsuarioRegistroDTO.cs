using System.ComponentModel.DataAnnotations;

namespace IT_Desarrollo_Back.DTOs
{
    public class UsuarioRegistroDTO
    {
        [StringLength(maximumLength: 100)]
        public string nombre { get; set; }
        [StringLength(maximumLength: 100)]
        public string apellido { get; set; }
        [StringLength(maximumLength:100)]
        public string email { get; set; }
        public int codigo_pais { get; set; }
        public int telefono { get; set; }
        public byte[]? img { get; set; }
        
        public string contrasena { get; set; }
        public int RolId { get; set; }
        public List<RespuestaRegistroDTO> Respuestas { get; set; }
    }
}
