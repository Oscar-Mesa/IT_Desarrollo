using IT_Desarrollo_Back.Entidades;

namespace IT_Desarrollo_Back.Models
{
    public class RespuestaApi
    {
        public string mensaje { get; set; }
        public Usuario usuario { get; set; }

        public RespuestaApi(string mensaje, Usuario usuario)
        {
            this.mensaje = mensaje;
            this.usuario = usuario;
        }
    }
}
