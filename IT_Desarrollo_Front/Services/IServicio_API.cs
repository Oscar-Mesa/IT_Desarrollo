using IT_Desarrollo_Front.Models;

namespace IT_Desarrollo_Front.Services
{
    public interface IServicio_API
    {
        Task<LoginResponse> PostLogin(string jsonData);
        Task<List<Usuarios>> GetUsuarios(string token);
        Task<List<Preguntas>> GetPreguntas();
        Task<LoginResponse> PostRegistro(string jsonData);
        Task<LoginResponse> PutPreguntas(string jsonData, string token);
        Task<PerfilResponse> GetPerfil(string token);
    }
}
